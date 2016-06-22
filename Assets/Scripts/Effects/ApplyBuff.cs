/*
 * ApplyBuff.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OmegaFramework
{
	[System.Serializable]
	public class ApplyBuff : Effect
	{
		Buff buff;

		public ApplyBuff(Buff buff)
		{
			this.buff = buff;
		}

		public Buff Buff
		{
			get {return buff;}
		}

		public override void Execute (UnitManager source, UnitManager target, Vector3 position)
		{
			if (buff.Source == null) {
				buff.Source = source;
			}
			target.AddBuff (buff);
		}
	}
}
