/*
 * Damage.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;
using System.Collections;

namespace OmegaFramework
{
	public class ApplyDamage : Effect
	{
		float damageRatio;

		public ApplyDamage(float damageRatio)
		{
			this.damageRatio = damageRatio;
		}

		public float DamageRatio
		{
			get {return damageRatio;}
		}

		public override void Execute (UnitManager source, UnitManager target, Vector3 position)
		{
			if (target != null)
				target.AddDamage (source, source.Stats.damage * damageRatio);
		}
	}
}
