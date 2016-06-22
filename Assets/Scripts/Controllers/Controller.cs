/*
 * Controller.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OmegaFramework
{
	/// <summary>
	/// Base Controller class for PlayerController and AIController.
	/// </summary>
	public abstract class Controller : MonoBehaviour
	{
		protected UnitManager UM;
	
		// Use to initilize references
		protected void Awake ()
		{
			UM = GetComponent<UnitManager> ();
		}
	}
}
