/*
 * UnitStats.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;
using System.Collections;

namespace OmegaFramework
{
	/// <summary>
	/// Unit stats.
	/// </summary>
	[System.Serializable]
	public class UnitStats
	{
		public float moveSpeed = 20.0f;
		public float damage = 10.0f;
		public float cooldownRate = 1.0f;
		public float maxHealth = 100.0f;
		public float regenThresh = 35.0f;
		public float regenDelay = 5.0f;
		public float regenRate = 1.0f;
		public float flinchDuration = 0.5f;
		public int killScore = 10;

		public UnitStats (float moveSpeed, float damage, float cooldownRate, float maxHealth, float minHealth,
		                 float regenDelay, float regenRate, float flinchDuration, int killScore)
		{
			this.moveSpeed = moveSpeed;
			this.damage = damage;
			this.cooldownRate = cooldownRate;
			this.maxHealth = maxHealth;
			this.regenThresh = minHealth;
			this.regenDelay = regenDelay;
			this.regenRate = regenRate;
			this.flinchDuration = flinchDuration;
			this.killScore = killScore;
		}

		public static UnitStats operator + (UnitStats us1, UnitStats us2)
		{
			return new UnitStats (us1.moveSpeed + us2.moveSpeed, us1.damage + us2.damage,
				us1.cooldownRate + us2.cooldownRate, us1.maxHealth + us2.maxHealth,
				us1.regenThresh + us2.regenThresh, us1.regenDelay + us2.regenDelay,
				us1.regenRate + us2.regenRate, us1.flinchDuration + us2.flinchDuration,
				us1.killScore + us2.killScore);
		}

		public static UnitStats operator * (UnitStats us1, UnitStats us2)
		{
			return new UnitStats (us1.moveSpeed * us2.moveSpeed, us1.damage * us2.damage,
				us1.cooldownRate * us2.cooldownRate, us1.maxHealth * us2.maxHealth,
				us1.regenThresh * us2.regenThresh, us1.regenDelay * us2.regenDelay,
				us1.regenRate * us2.regenRate, us1.flinchDuration * us2.flinchDuration,
				us1.killScore * us2.killScore);
		}
	}
}
