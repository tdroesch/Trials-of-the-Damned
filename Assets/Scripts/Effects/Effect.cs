/*
 * Effect.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;
using System.Collections;

namespace OmegaFramework
{
	[System.Serializable]
	/// <summary>
	/// Effect class that is triggered by Ability.
	/// </summary>
	public abstract class Effect
	{
		public abstract void Execute (UnitManager source, UnitManager target, Vector3 position);
	}
}
