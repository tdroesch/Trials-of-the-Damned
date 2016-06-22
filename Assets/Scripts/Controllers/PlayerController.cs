/*
 * PlayerController.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OmegaFramework
{
	/// <summary>
	/// Player controller for passing keypresses to the UnitManager.
	/// </summary>
	public class PlayerController : Controller
	{
		//The max distance from the AI to a waypoint for it to continue to the next waypoint
//		public float nextWaypointDistance = 10;
		//The Rate that the computer refreshes it's path
//		public float researchRate = 1.0f;
//		float searchTimer;
		//The waypoint we are currently moving towards
//		private int currentWaypoint = 0;
		
		// Use this for initialization
		void Start ()
		{
	
		}

		void Update ()
		{
//			if (searchTimer >= 0)
//				searchTimer -= Time.deltaTime;
//			CheckMovement ();  // Check to see if a move command was issued
			CheckAbilities (); // Check to see if any of the abilities keys have been used
		}


		// Don't think this is needed anymore.
//		void CheckMovement ()
//		{
//			if (Input.GetMouseButtonDown (0) || (Input.GetMouseButton (0) && searchTimer <= 0)) {
//				//Move
//				Vector3 mousePos = RuntimeUtilities.GetMousePosition ();
//				if (mousePos.magnitude < Mathf.Infinity) {
//					//UM.AddAction (ActionType.MOVE, mousePos);
//				}
//				searchTimer = researchRate;
//			}
//		}

		void CheckAbilities ()
		{
			List<AbilityInfo> abilityInfo = UM.GetAbilityInfo ();
			for (int i = 0; i < abilityInfo.Count; i++) {
				AbilityInfo a = abilityInfo [i];
				if (Input.GetButton (a.buttonName)) {
					if (a.cooldown <= 0) {
						//Use Ability
						UM.AddAction (RuntimeUtilities.GetMousePosition (), i);
					}
				}
			}
		}
	}
}
