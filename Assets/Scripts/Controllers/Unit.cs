/*
 * Unit.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;
using System.Collections.Generic;

namespace OmegaFramework
{
	/// <summary>
	/// Unit holds UnitStats and UnitFlags to represent the current state of the unit.
	/// </summary>
	[System.Serializable]
	public class Unit
	{
		[SerializeField]
		UnitStats baseStats;
		[SerializeField]
		UnitFlags baseFlags;
	
		List<UnitStats> flatModifiers = new List<UnitStats>();
		List<UnitStats> percentModifiers = new List<UnitStats>();
		List<UnitFlags> flagModifiers = new List<UnitFlags> ();

		/// <summary>
		/// Get the base stats combined with flat and percent modifiers.
		/// </summary>
		/// <value>The stats.</value>
		internal UnitStats Stats {
			get {
				UnitStats stats = baseStats;
				foreach (UnitStats f in flatModifiers) {
					stats += f;
				}
				UnitStats multipier = new UnitStats (1, 1, 1, 1, 1, 1, 1, 1, 1);
				foreach (UnitStats p in percentModifiers) {
					multipier += p;
				}
				return stats * multipier;
			}
		}

		/// <summary>
		/// Get all the flags for this unit.
		/// </summary>
		/// <value>The flags.</value>
		internal int Flags {
			get {
				int flags = baseFlags.Flags;
				foreach (UnitFlags f in flagModifiers) {
					flags |= f.Flags;
				}
				return flags;
			}
		}

		/// <summary>
		/// Adds flat stats to the unit.
		/// </summary>
		/// <returns><c>true</c>, if flat stats was added, <c>false</c> otherwise.</returns>
		/// <param name="f">The UnitStats containing flat stats modifiers.</param>
		internal bool AddFlatStats (UnitStats f)
		{
			if (f != null) {
				flatModifiers.Add (f);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Removes a flat stats modifier.
		/// </summary>
		/// <returns><c>true</c>, if flat stats was removed, <c>false</c> otherwise.</returns>
		/// <param name="f">The UnitStats containing flat stats modifiers.</param>
		internal bool RemoveFlatStats (UnitStats f)
		{
			return flatModifiers.Remove (f);
		}

		/// <summary>
		/// Adds a percent stats modifier.
		/// </summary>
		/// <returns><c>true</c>, if percent stats was added, <c>false</c> otherwise.</returns>
		/// <param name="p">The UnitStats containing percent stats modifiers.</param>
		internal bool AddPercentStats (UnitStats p)
		{
			if (p != null) {
				percentModifiers.Add (p);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Removes a percent stats modifier.
		/// </summary>
		/// <returns><c>true</c>, if percent stats was removed, <c>false</c> otherwise.</returns>
		/// <param name="p">The UnitStats containing percent stats modifiers.</param>
		internal bool RemovePercentStats (UnitStats p)
		{
			return percentModifiers.Remove (p);
		}

		/// <summary>
		/// Adds flags to the unit.
		/// Adding a CC_IMMUNE_FLAG will clear all crowd control type flags.
		/// Adding a DEAD_FLAG will remove all other flags.
		/// </summary>
		/// <returns><c>true</c>, if flags was added, <c>false</c> otherwise.</returns>
		/// <param name="f">The UnitFlags containing flag modifiers.</param>
		internal bool AddFlags (UnitFlags f)
		{
			//Add conditional effects for when CC_IMMUNE flag or DEAD flag is passed
			if (f != null) {
				//if dead flag is passed, remove all flags and add dead flag and return true
				if ((f.Flags & UnitFlags.DEAD_FLAG) == UnitFlags.DEAD_FLAG) {
					flagModifiers.Clear ();
					flagModifiers.Add (f);
					return true;
				}
				//if cc_immune flag passed remove all unitflags with stun, silence, or imobilized, add flag and return true
				if ((f.Flags & UnitFlags.CC_IMMUNE_FLAG) == UnitFlags.CC_IMMUNE_FLAG) {
					List<UnitFlags> removeFlags = new List<UnitFlags> ();
					foreach (UnitFlags uf in flagModifiers) {
						if ((uf.Flags & (UnitFlags.STUNNED_FLAG | UnitFlags.SILENCED_FLAG | UnitFlags.IMMOBILIZED_FLAG)) != 0) {
							removeFlags.Add (uf);
						}
					}
					foreach (UnitFlags uf in removeFlags) {
						flagModifiers.Remove (uf);
					}
					flagModifiers.Add (f);
					return true;
				}
				//if a cc flag is passed and unit is currently cc immune return false;
				if ((Flags & UnitFlags.CC_IMMUNE_FLAG) == UnitFlags.CC_IMMUNE_FLAG && (f.Flags & (UnitFlags.STUNNED_FLAG | UnitFlags.SILENCED_FLAG | UnitFlags.IMMOBILIZED_FLAG)) != 0) {
					return false;
				}
				flagModifiers.Add (f);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Removes flags from the unit.
		/// </summary>
		/// <returns><c>true</c>, if flags was removed, <c>false</c> otherwise.</returns>
		/// <param name="f">The UnitFlags containing flag modifiers.</param>
		internal bool RemoveFlags (UnitFlags f)
		{
			if (f != null) {
				return flagModifiers.Remove (f);
			} else
				return false;
		}
		float health = 100.0f;
		float lastDmgTimer = 0;

		internal void InitHealth(){
			health = Stats.maxHealth;
		}

		/// <summary>
		/// Gets the current health.
		/// </summary>
		/// <value>The current health.</value>
		public float CurrentHealth {
			get {
				return health;
			}
		}
	
		// Update is called once per frame
		internal void Update ()
		{
			RegenHealth ();
			if (health > Stats.maxHealth)
				health = Stats.maxHealth;
		}

		/// <summary>
		/// Regenerates the health if below the minimum and hasn't been damaged recently, and removes flinch.
		/// </summary>
		void RegenHealth ()
		{
			//If Alive block
			if ((Flags & UnitFlags.DEAD_FLAG) == 0) {
				//Regen Health block
				{
					lastDmgTimer += Time.deltaTime;
					if (lastDmgTimer >= Stats.regenDelay && health < Stats.regenThresh) {
						health += Stats.regenRate * Time.deltaTime;
					}
					if ((Flags & UnitFlags.CAN_FLINCH_FLAG) != 0 && lastDmgTimer >= Stats.flinchDuration) {
						baseFlags.Flags &= ~UnitFlags.STUNNED_FLAG;
					}
				}
			}
		}

		/// <summary>
		/// Deal damage to the unit.
		/// </summary>
		/// <returns><c>true</c>if the unit dies.<c>false</c> otherwise.</returns>
		/// <param name="dmg">The damage to be done.</param>
		public bool DealDamage (float dmg)
		{
			health -= dmg;
			lastDmgTimer = 0;
			if ((Flags & UnitFlags.CAN_FLINCH_FLAG) != 0) {
				baseFlags.Flags |= UnitFlags.STUNNED_FLAG;
			}
			if (health <= 0) {
				baseFlags.Flags |= UnitFlags.DEAD_FLAG;
				flagModifiers.Clear ();
				return true;
			} else
				return false;
		}
	}
}
