/*
 * UnitFlags.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;

namespace OmegaFramework
{
	/// <summary>
	/// Unit flags.
	/// </summary>
	[System.Serializable]
	public class UnitFlags
	{
		[SerializeField]
		int flags = 0;

		public int Flags {
			get { return flags; }
			set { flags = value; }
		}

		public UnitFlags (int flags)
		{
			this.flags = flags;
		}
	
		// Flags determine the state of the unit.
		public const int DEAD_FLAG = 1 << 0;
		//is dead.
		public const int STUNNED_FLAG = 1 << 1;
		//is stunned
		public const int SILENCED_FLAG = 1 << 2;
		//is silenced
		public const int IMMOBILIZED_FLAG = 1 << 3;
		//is immobilized
		public const int CC_IMMUNE_FLAG = 1 << 4;
		//is immune to CC
		public const int DAMGAGE_IMMUNE_FLAG = 1 << 5;
		//is immune to damage
		public const int MOVE_BLOCK_FLAG = 1 << 6;
		//is being moved by non-move command
		public const int ABILITY_BLOCK_FLAG = 1 << 7;
		//another ability is blocking the use of abilities
		public const int CAN_FLINCH_FLAG = 1 << 8;
		//will flinch(mini stun) when damaged
		public const int MOVING_FLAG = 1 << 9;
	}
}
