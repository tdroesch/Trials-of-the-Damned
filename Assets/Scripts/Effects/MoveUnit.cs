/*
 * MoveUnit.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OmegaFramework
{
	public class MoveUnit : Effect
	{
		float distance;
		float duration;
		bool pathfinding;

		public MoveUnit(float distance, float duration, bool pathfinding)
		{
			this.distance = distance;
			this.duration = duration;
			this.pathfinding = pathfinding;
		}

		public float Distance
		{
			get {return distance;}
		}

		public float Duration
		{
			get {return duration;}
		}

		public bool Pathfinding
		{
			get {return pathfinding;}
		}

		public override void Execute (UnitManager source, UnitManager target, Vector3 position)
		{
			Vector3 moveTo = pathfinding ? (position - target.transform.position) * Mathf.Sign (distance) + target.transform.position : 
										   (position - target.transform.position).normalized * distance + target.transform.position;
			if (!pathfinding)
				moveTo.y = position.y;
			target.MoveUnit (moveTo, pathfinding, duration, source);
		}
	}
}

