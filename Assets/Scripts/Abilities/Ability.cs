/*
 * Ability.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;
using System.Collections.Generic;

namespace OmegaFramework
{
	[System.Serializable]
	/// <summary>
	/// Ability class that holds:
	/// 	Key Press to acctivate it.
	/// 	Name of the ability.
	/// 	The range it can be used.
	/// 	The abilities cooldown.
	/// 	The abilities icons.
	/// 	The effect the ability executes.
	/// </summary>
	public class Ability
	{
		[SerializeField]
		string buttonName;
		[SerializeField]
		string abilityName = "None";
		[SerializeField]
		float range = 1.0f;
		[SerializeField]
		float cooldownTime = 1.0f;
		float cooldown = 0.0f;
		[SerializeField]
		Sprite icon;
		[SerializeField]
		Sprite disabledIcon;
	
		AbilityPhase current;
		[SerializeField]
		AbilityPhase windup;
		[SerializeField]
		AbilityPhase main;
		[SerializeField]
		AbilityPhase winddown;
		[SerializeField]
		int disableFlags;
		UnitFlags abilityFlags;
	
		Vector3 targetPos;

		[SerializeField]
		bool requiresLOS;

		public Ability(){
			windup = new AbilityPhase ();
			main = new AbilityPhase ();
			winddown = new AbilityPhase ();
			windup.NextPhase = main;
			main.NextPhase = winddown;
		}

		/// <summary>
		/// Execute the Abilities effect.
		/// </summary>
		/// <param name="name">The ability name.</param>
		public UnitFlags Execute (UnitManager source, Vector3 mousePos)
		{
			if ((source.Flags & disableFlags) == 0) {
				cooldown = cooldownTime / source.Stats.cooldownRate;
				targetPos = mousePos;
				abilityFlags = new UnitFlags(windup.Start (source, mousePos, ref current));
				if (current.endOnCollision) {
					source.AddCollisionCallback (NextPhase);
				}
				return abilityFlags;
			} else {
				return null;
			}
		}

		public UnitFlags Halt ()
		{
			targetPos = Vector3.zero;
			current = null;
			return abilityFlags;
		}

		public UnitFlags NextPhase (UnitManager source)
		{
			if (current != null) {
				abilityFlags.Flags = current.End (source, targetPos, ref current);
				if (current == null) {
					return abilityFlags;
				} else {
					if (current.endOnCollision) {
						source.AddCollisionCallback (NextPhase);
					}
					return null;
				}
			}
			return null;
		}

	
		[System.Serializable]
		public class AbilityPhase
		{
			[SerializeField]
			bool automatic;
			[SerializeField]
			internal bool endOnCollision;
			AbilityPhase nextPhase;
			[SerializeField]
			int setFlags;
			[SerializeField]
			int cancelFlags;
			[SerializeField]
			List<SerializableEffect> selfEffects;
			[SerializeField]
			List<SerializableEffect> targetEffects;
			[SerializeField]
			string animation;
			[SerializeField]
			GameObject vfx;
			[SerializeField]
			Vector3 vfxPosition;
			[SerializeField]
			Vector3 vfxRotation;
			[SerializeField]
			AudioClip sfx;
			[SerializeField]
			float volume;

			public bool Automatic {
				get {
					return automatic;
				}
			}

			public int CancelFlags {
				get {
					return cancelFlags;
				}
			}

			public AbilityPhase NextPhase {
				get {
					return nextPhase;
				}
				set {
					nextPhase = value;
				}
			}

			internal int Start (UnitManager source, Vector3 targetPos, ref AbilityPhase current)
			{
				source.TriggerAnimation (animation);
				source.InstantiateVFX (vfx, vfxPosition, vfxRotation);
				source.PlaySFX (sfx, volume);
				foreach (SerializableEffect e in selfEffects) {
					e.Execute (source, source, targetPos);
				}
				foreach (SerializableEffect e in targetEffects) {
					e.Execute (source, null, targetPos);
				}
				current = this;
				return setFlags;
			}

			internal int End (UnitManager source, Vector3 targetPos, ref AbilityPhase current)
			{
				if (nextPhase != null) {
					return nextPhase.Start (source, targetPos, ref current);
				} else {
					current = null;
					return 0;
				}
			}
		}
	
	
		//	protected void spawnParticle(){
		//		if(particleFX){
		//			Vector3 particlePosition = this.gameObject.transform.position;
		//			Quaternion particleRotation = this.gameObject.transform.rotation;
		//			GameObject p = (GameObject)Instantiate(particleFX, particlePosition, particleRotation);
		//			p.transform.parent = this.gameObject.transform;
		//		}
		//	}
		//	protected void playSoundFX(){
		//		if (soundFX) {
		//			GetComponent<AudioSource>().PlayOneShot (soundFX);
		//		}
		//	}
	
		public AbilityInfo Info ()
		{
			return new AbilityInfo (abilityName, buttonName, range, cooldown, cooldown > 0 ? disabledIcon : icon, requiresLOS);
		}

		internal void Start ()
		{
	
		}

		internal UnitFlags Update (UnitManager source)
		{
			if (cooldown > 0.0f)
				cooldown -= Time.deltaTime;
			if (current != null) {
				//If any part of flags mathes the current phase cancel flags stop the ability.
				if ((source.Flags & current.CancelFlags) != 0) {
					return Halt ();
				}
				
				//If the current phase is automatic, end it and start the next phase.
				if (current.Automatic) {
					return NextPhase (source);
				}
			}
			return null;
		}
	}

	/// <summary>
	/// Ability info.
	/// </summary>
	public class AbilityInfo
	{
		public AbilityInfo (string a, string b, float r, float c, Sprite i, bool l)
		{
			abilityName = a;
			buttonName = b;
			range = r;
			cooldown = c;
			icon = i;
			requiresLOS = l;

		}

		public string abilityName;
		public string buttonName;
		public float range;
		public float cooldown;
		public Sprite icon;
		public bool requiresLOS;
	}
}


