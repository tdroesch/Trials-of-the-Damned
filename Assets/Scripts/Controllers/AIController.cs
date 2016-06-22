/*
 * AIController.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OmegaFramework
{
	/// <summary>
	/// AI controller for giving basic AI to the UnitManager.
	/// </summary>
	public class AIController : Controller
	{
		[HideInInspector]
		public GameObject target;
		//The Rate that the computer refreshes it's path
//		public float researchRate = 1.0f;
//		float searchTimer;
		Vector3 originOffset;
		Vector3 targetOffset;
	
	
		// Use this for initialization
		void Start ()
		{
//			searchTimer = researchRate;
			target = GameObject.FindGameObjectWithTag ("Player");
			originOffset = new Vector3 ();
			originOffset.y = GetComponent<NavMeshAgent> ().height / 2;
			targetOffset = new Vector3 ();
			targetOffset.y = target.GetComponent<NavMeshAgent> ().height / 2;
		}
		
		// Update is called once per frame
		void Update ()
		{
//			if (searchTimer >= 0)
//				searchTimer -= Time.deltaTime;
			// If the ai has a target
			if (target != null) {
				// Is target in line of sight?
				RaycastHit hit;
				if (Physics.Raycast (transform.position+originOffset, (target.transform.position+targetOffset) - (transform.position+originOffset), out hit, RuntimeUtilities.GROUND_LAYER|RuntimeUtilities.OBSTACLE_LAYER|RuntimeUtilities.PLAYER_LAYER)) {
					if (LayerMask.GetMask(LayerMask.LayerToName(hit.collider.gameObject.layer)) == RuntimeUtilities.PLAYER_LAYER) {
						// is target in range of an ability that is off cooldown
						List<AbilityInfo> abilityInfo = UM.GetAbilityInfo ();
						for (int i = 0; i < abilityInfo.Count; i++) {
							AbilityInfo a = abilityInfo [i];
							if (hit.distance <= a.range && a.cooldown <= 0) {
								/************************Add priority sorting to the AI*********************************/
								UM.AddAction (target.transform.position, i);
								return;
							}
						}
					}
				}
				// No? then issue a move command towards the target
				else {
					List<AbilityInfo> abilityInfo = UM.GetAbilityInfo ();
					for (int i = 0; i < abilityInfo.Count; i++) {
						AbilityInfo a = abilityInfo [i];
						if (!a.requiresLOS && a.cooldown <= 0) {
							/************************Add priority sorting to the AI*********************************/
							UM.AddAction (target.transform.position, i);
							return;
						}
					}
				}
			}
		}
	}
}