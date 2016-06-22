/*
 * Movement.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;
using System.Collections;

namespace OmegaFramework
{
	/// <summary>
	/// Movement combines NavMesh pathfinding movement with straight line movement that will cross gaps while keeping the agent on the NavMesh.
	/// </summary>
	[System.Serializable]
	public class Movement
	{
		/// <summary>
		/// Nodes used for staight line movement.
		/// </summary>
		internal struct MovementNode
		{
			public Vector3 position;
			public bool isOffMeshNode;

			public MovementNode (Vector3 pos, bool offMesh)
			{
				position = pos;
				isOffMeshNode = offMesh;
			}
		}

		Transform transform;
		NavMeshAgent agent;
		Rigidbody rigidbody;
		float speed;
		MovementNode[] nodes;
		int targetNode;
		bool pathfinding;
		int stopFlags;
		internal UnitFlags movementFlag;

		internal Movement (Transform transform, NavMeshAgent agent, Rigidbody rigidbody)
		{
			this.transform = transform;
			this.agent = agent;
			this.rigidbody = rigidbody;
			this.stopFlags = UnitFlags.DEAD_FLAG | UnitFlags.STUNNED_FLAG | UnitFlags.IMMOBILIZED_FLAG | UnitFlags.MOVE_BLOCK_FLAG;
			movementFlag = new UnitFlags (0);
		}
	
		// Update is called once per frame by the UnitManager.
		internal bool Update (float speed, int unitFlags)
		{
			rigidbody.Sleep ();
			if (nodes != null || agent.hasPath) {
				// If using pathfinding update the NavMeshAgent.
				if (pathfinding) {
					agent.updateRotation = true;
					agent.speed = speed;
					if ((unitFlags & stopFlags) != 0) {
						agent.Stop ();
						agent.ResetPath ();
					}
					if (agent.hasPath) {
						movementFlag.Flags = UnitFlags.MOVING_FLAG;
						return true;
					} else {
						movementFlag.Flags = 0;
						return false;
					}
				} else {
					agent.updateRotation = false;
					if ((unitFlags & stopFlags) == 0) {
						// Rotate the agent towards the direction it is moving.
						transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (nodes [targetNode].position - transform.position, Vector3.up), agent.angularSpeed);
					}
					// If the target node is off the NavMesh, movedirectly towards it, else make sure the agent is on the NavMesh and set it's velocity.
					if (nodes [targetNode].isOffMeshNode) {
						agent.updatePosition = false;
						transform.position = Vector3.MoveTowards (transform.position, nodes [targetNode].position, this.speed * Time.deltaTime);
					} else {
						if (!agent.isOnNavMesh) {
							NavMeshHit hit;
							if (NavMesh.SamplePosition (transform.position, out hit, agent.height, NavMesh.AllAreas)) {
								agent.Warp (hit.position);
							} else {
								Debug.LogWarning ("Agent is too far off NavMesh to find a position during Movement");
								ResetMovement ();
								movementFlag.Flags = 0;
								return false;
							}
						}
						agent.updatePosition = true;
						agent.velocity = (nodes [targetNode].position - transform.position).normalized * this.speed;
					}
					// If the agent has reached its target get the next node.
					if (Vector3.Distance (transform.position, nodes [targetNode].position) < agent.radius / 2) {
						targetNode++;
						if (targetNode < nodes.Length) {
							if (nodes [targetNode].isOffMeshNode) {
//								agent.updatePosition = false;
//								agent.updateRotation = false;
							} else {
								agent.Warp (transform.position);
//								agent.updatePosition = true;
//								agent.updateRotation = true;
							}
						}
					}
					// If there are no more nodes left in the path, reset movement.
					if (targetNode < nodes.Length) {
						movementFlag.Flags = UnitFlags.MOVING_FLAG;
						return true;
					} else {
						ResetMovement ();
						movementFlag.Flags = 0;
						return false;
					}
				}
			} else {
				movementFlag.Flags = 0;
				return false;
			}
		}

		/// <summary>
		/// Sets the destination.
		/// </summary>
		/// <param name="destination">Destination.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="pathfinding">If set to <c>true</c> use pathfinding.</param>
		internal void SetDestination (Vector3 destination, float duration, bool pathfinding)
		{
			this.pathfinding = pathfinding;
			if (pathfinding) {
				agent.SetDestination (destination);
				agent.Resume ();
			} else {
				this.speed = Vector3.Distance (destination, transform.position) / (duration == 0 ? Time.deltaTime : duration);
				this.nodes = GetNodes (transform.position, destination);
				this.targetNode = 0;
				agent.ResetPath ();
				agent.Stop ();
			}
		}

		/// <summary>
		/// Resets the movement.
		/// </summary>
		void ResetMovement ()
		{
			agent.ResetPath ();
			agent.Stop ();
			this.pathfinding = true;
			this.speed = 0;
			this.nodes = null;
			this.targetNode = 0;
		}

		/// <summary>
		/// Sets the facing towards point.
		/// </summary>
		/// <param name="target">Target point to face.</param>
		internal void SetFacing(Vector3 target){
			// Rotate the agent towards the direction it is moving.
			transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (target - transform.position, Vector3.up), 360f);

		}

		/// <summary>
		/// Gets the movement nodes for staight line movement.
		/// </summary>
		/// <returns>The nodes.</returns>
		/// <param name="start">Start of the path.</param>
		/// <param name="end">End of the path.</param>
		MovementNode[] GetNodes (Vector3 start, Vector3 end)
		{
			NavMeshHit hit;
			// Check if there is a straight line path from the start to end.
			if (NavMesh.Raycast (start, end, out hit, NavMesh.AllAreas)) {
				// Save the vector from the start to where the raycast hit.
				Vector3 vec = hit.position - start;
				// Distance from the hit to the end the ray was trying to get to.
				float distance = Vector3.Distance (hit.position, end);
				// How many times will we sample the gap between the hit and end.
				int samples = Mathf.FloorToInt (distance / agent.height);
				// How far apar are the samples
				float sampleRate = distance / samples;
				NavMeshHit sampleHit;
				// Sample the Navmesh until we get to the end
				for (int i = 1; i <= samples; i++) {
					if (NavMesh.SamplePosition (hit.position + (end - hit.position).normalized * i * sampleRate, out sampleHit, sampleRate, NavMesh.AllAreas)) {
						// If the sample hit is same as the hit ignore it.
						if (hit.position != sampleHit.position) {
							Vector3 flatVec = vec;
							flatVec.y = 0;
							Vector3 sampVec = sampleHit.position - hit.position;
							sampVec.y = 0;
							// Check to see if the sample hit is inline with the hit and the start.
							if (flatVec.normalized == sampVec.normalized) {
								// Check if the sample is below the hit.
								if (sampleHit.position.y < hit.position.y) {
									// Create an off mesh link over the obstacle
									MovementNode[] temp = GetNodes (sampleHit.position, end);
									MovementNode[] _nodes = new MovementNode[temp.Length + 2];
									temp.CopyTo (_nodes, 2);
									_nodes [0] = new MovementNode (hit.position, false);
									_nodes [1] = new MovementNode (sampleHit.position, true);
									return _nodes;
								} else {
									return new MovementNode[] { new MovementNode (hit.position, false) };
								}
							}
						}
					}
				}
				return new MovementNode[] { new MovementNode (hit.position, false) };
			} else {
				if (NavMesh.SamplePosition (end, out hit, agent.height, NavMesh.AllAreas)) {
					return new MovementNode[] { new MovementNode (hit.position, false) };
				} else {
					Debug.LogWarning ("Agent target is too far from navmesh.  Fix your code.");
					return new MovementNode[] { new MovementNode (end, false) };
				}
			}
		}
	}
}
