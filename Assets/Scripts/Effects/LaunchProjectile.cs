/*
 * LaunchProjectile.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OmegaFramework
{
	// How do I incorporate the projectile into the Unitmanager system.
	// is a projectile a mangaged unit without any unit stats.
	// How do I incorporate collision into the unitmanager system.
	public class LaunchProjectile : Effect
	{
		Rigidbody projectile;
		float speed;
		float duration;

		public LaunchProjectile(Rigidbody projectile, float speed, float duration)
		{
			this.projectile = projectile;
			this.speed = speed;
			this.duration = duration;
		}

		public Rigidbody Projectile
		{
			get {return projectile;}
		}

		public float Speed
		{
			get {return speed;}
		}

		public float Duration
		{
			get {return duration;}
		}

		public override void Execute (UnitManager source, UnitManager target, Vector3 position)
		{
			Rigidbody p = GameObject.Instantiate (projectile, target.gameObject.transform.position, Quaternion.LookRotation(position - target.transform.position, Vector3.up)) as Rigidbody;
			Projectile pp = p.gameObject.GetComponent<Projectile> ();
			pp.Init (speed, duration, source, position);
		}
	}
}
