/*
 * UnitManager.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OmegaFramework
{
	/// <summary>
	/// UnitManager binds together unit stats, movment, animation, visual effects, audio effects, and abilities.
	/// </summary>
	[RequireComponent (typeof(NavMeshAgent))]
	[RequireComponent (typeof(Animator))]
	public class UnitManager : MonoBehaviour
	{
		[SerializeField]
		Unit unit;
		Movement movement;
		Animator animator;
		AudioSource audioSource;
		[SerializeField]
		List<Ability> abilities;
		[SerializeField]
		List<Buff> buffs;
		[SerializeField]
		float despawnTime;
		[SerializeField]
		string deathTrigger;
		[SerializeField]
		string flinchTrigger;
		[SerializeField]
		string movingBool;
	
		List<Buff> pendingAdd;
		List<Buff> pendingRemove;

		/// <summary>
		/// Gets the stats from the unit excluding current health.
		/// </summary>
		/// <value>The UnitStats from the unit that combines the base stats with any modifiers.</value>
		public UnitStats Stats {
			get { return unit.Stats; }
		}

		/// <summary>
		/// Gets the flags from the unit. This value can be compared against the constanst from the UnitFlags class.
		/// </summary>
		/// <value>The flags.</value>
		public int Flags {
			get { return unit.Flags; }
		}

		/// <summary>
		/// Gets the current health from the unit.
		/// </summary>
		/// <value>The health.</value>
		public float Health {
			get { return unit.CurrentHealth; }
		}

		/// <summary>
		/// Helper class for passing actions into the UnitMangaer.
		/// </summary>
		class Action
		{
			public Action (Vector3 t, int a)
			{
				target = t;
				abilityIndex = a;
			}

			public Vector3 target;
			public int abilityIndex;
		}

		Action nextAction;
	
		// Use for GetComponent Calls
		void Awake ()
		{
			animator = GetComponent<Animator> ();
			audioSource = Camera.main.GetComponent<AudioSource> ();
			movement = new Movement (transform, GetComponent<NavMeshAgent> (), GetComponent<Rigidbody>());
			unit.AddFlags (movement.movementFlag);
			unit.InitHealth ();
			pendingAdd = new List<Buff> ();
			pendingRemove = new List<Buff> ();
			foreach (Buff b in buffs) {
				unit.AddFlatStats (b.FlatStats);
				unit.AddPercentStats (b.PercentStats);
				unit.AddFlags (b.Flags);
				if (b.AnimationBool != null && b.AnimationBool != "") {
					animator.SetBool (b.AnimationBool, true);
				}
			}
		}
	
		// Update is called once per frame
		void Update ()
		{
			unit.Update ();
			
			// Update each buff currently attached to the UnitManager;
			// Buffs that have timed out get removed;
			foreach (Buff b in buffs) {
				if (b.Update (this) <= 0.0f) {
					RemoveBuff (b);
				}
			}
			// Update the effects of the abilities.
			// If remove the flags applied by any abilities that stop this frame.
			foreach (Ability a in abilities) {
				unit.RemoveFlags (a.Update (this));
			}
	
			// If there is a pending action execute it.
			if (nextAction != null) {
				Execute ();
			}
	
			// Update the unit movment.
			animator.SetBool(movingBool, movement.Update (unit.Stats.moveSpeed, unit.Flags));
		}
	
		// LateUpdate is called once per frame after all updates have been called.
		void LateUpdate ()
		{
			// Remove any buffs that expired this frame.
			foreach (Buff b in pendingRemove) {
				//Remove stat and flag mods from unit
				unit.RemoveFlatStats (b.FlatStats);
				unit.RemovePercentStats (b.PercentStats);
				unit.RemoveFlags (b.Flags);
				if (b.AnimationBool != null && b.AnimationBool != "") {
					animator.SetBool (b.AnimationBool, false);
				}
				buffs.Remove (b);
			}
			pendingRemove.Clear ();
	
			// Add any buffs that were applied this frame.
			foreach (Buff b in pendingAdd) {
				//Add stat and flag mods to unit
				unit.AddFlatStats (b.FlatStats);
				unit.AddPercentStats (b.PercentStats);
				unit.AddFlags (b.Flags);
				if (b.AnimationBool != null && b.AnimationBool != "") {
					animator.SetBool (b.AnimationBool, true);
				}
				buffs.Add (b);
			}
			pendingAdd.Clear ();
		}

		/// <summary>
		/// Adds an action to be executed on the next frame.
		/// </summary>
		/// <param name="mousePos">Mouse position when the ability was activated.</param>
		/// <param name="abilityIndex">Ability index that determines which ability will be activated.</param>
		public void AddAction (Vector3 mousePos, int abilityIndex)
		{
			nextAction = new Action (mousePos, abilityIndex);
		}

		/// <summary>
		/// Adds damage to the unit.
		/// If the damage would reduce the unit's health to 0 or less kill the unit.
		/// </summary>
		/// <param name="source">The UnitManager that caused the damage.</param>
		/// <param name="d">The amount of damage.</param>
		public void AddDamage (UnitManager source, float d)
		{
			if ((unit.Flags & UnitFlags.DAMGAGE_IMMUNE_FLAG) == 0) {
				source.OnHit ();
				// If the damage kill the unit.
				if (unit.DealDamage (d)) {
					source.OnKill (unit.Stats.killScore);
					animator.SetTrigger (deathTrigger);
					GetComponent<Collider> ().enabled = false;
					GetComponent<NavMeshAgent> ().enabled = false;
					foreach (Buff b in buffs) {
						RemoveBuff (b);
					}
					Destroy (this.gameObject, despawnTime);
				}
			}

//			Debug.Log (this.name + " " + Health);
	
			/************* Adjust the length of stun animation to match duration *************/
			if ((unit.Flags & UnitFlags.STUNNED_FLAG) == UnitFlags.STUNNED_FLAG) {
				animator.SetTrigger (flinchTrigger);
			}
	
		}

		/// <summary>
		/// Adds a buff or debuff to the unit.
		/// </summary>
		/// <param name="buff">Buff.</param>
		public void AddBuff (Buff buff)
		{
			pendingAdd.Add ((Buff)buff.Clone ());
		}

		/// <summary>
		/// Removes a buff or debuff from the unit.
		/// </summary>
		/// <param name="buff">Buff.</param>
		public void RemoveBuff (Buff buff)
		{
			pendingRemove.Add (buff);
		}
	
		public delegate void KillDelegate (float score);
		event KillDelegate killed;

		void OnKill (int killScore)
		{
			// Call Kill Callbacks
			if (killed != null) {
				killed (killScore);
			}
		}
		public void AddKillCallback(KillDelegate callback){
			killed += callback;
		}

		public delegate void HitDelegate ();
		event HitDelegate hit;

		void OnHit(){
			// Call Hit callbacks
			if (hit != null) {
				hit ();
			}
		}
		public void AddHitCallback(HitDelegate callback){
			hit += callback;
		}

		/// <summary>
		/// Get information about the abilities on this unit.
		/// </summary>
		/// <returns>List of all abilities button, cooldown, and range.</returns>
		public List<AbilityInfo> GetAbilityInfo ()
		{
			List<AbilityInfo> info = new List<AbilityInfo> (abilities.Count);
			foreach (Ability a in abilities) {
				info.Add (a.Info ());
			}
			return info;
		}

		/// <summary>
		/// Get information about buffs currently effecting this unit.
		/// </summary>
		/// <returns>List of all the buffs duration and source.</returns>
		public List<BuffInfo> GetBuffInfo ()
		{
			List<BuffInfo> info = new List<BuffInfo> (buffs.Count);
			foreach (Buff b in buffs) {
				info.Add (b.Info ());
			}
			return info;
		}

				
		/// <summary>
		/// Attempt to execute the next action if there is one
		/// </summary>
		void Execute ()
		{
			UnitFlags abilityFlags = abilities [nextAction.abilityIndex].Execute (this, nextAction.target);
			if (abilityFlags != null) {
				movement.SetFacing (nextAction.target);
				unit.AddFlags (abilityFlags);
				nextAction = null;
			}
		}

		/// <summary>
		/// Moves the unit to the destination with movement class.
		/// </summary>
		/// <param name="destination">Destination.</param>
		/// <param name="pathfinding">If set to <c>true</c> use pathfinding movement.</param>
		/// <param name="duration">Duration of the movment for non-pathfinding movement.</param>
		internal void MoveUnit (Vector3 destination, bool pathfinding, float duration, UnitManager source)
		{
			if (source != this && (Flags & UnitFlags.CC_IMMUNE_FLAG) != 0) {
				return;
			} else {
				movement.SetDestination (destination, duration, pathfinding);
			}
		}

		/// <summary>
		/// Triggers the animation.
		/// </summary>
		/// <param name="animation">Animation.</param>
		internal void TriggerAnimation (string animation)
		{
			if (animation != null && animation != "") {
				animator.SetTrigger (animation);
			}
		}

		/// <summary>
		/// Instantiates the Visual Effect.
		/// </summary>
		/// <param name="vfx">VFX prefab.</param>
		/// <param name="localPosition">Local position.</param>
		/// <param name="localRotation">Local rotation.</param>
		internal void InstantiateVFX (GameObject vfx, Vector3 localPosition, Vector3 localRotation)
		{
			if (vfx != null) {
				GameObject fx = Instantiate (vfx, transform.position + localPosition, transform.localRotation * Quaternion.Euler (localRotation)) as GameObject;
				fx.transform.parent = transform;
			}
		}

	
		/// <summary>
		/// Plays the Sound Effect.
		/// </summary>
		/// <param name="sfx">SFX file.</param>
		/// <param name="volume">Volume.</param>
		internal void PlaySFX (AudioClip sfx, float volume)
		{
			if (sfx != null) {
				audioSource.PlayOneShot (sfx, volume);
			}
		}

		public void NextAbilityPhase(int abilityIndex){
			abilities [abilityIndex].NextPhase (this);
		}

		public delegate UnitFlags CollisionDelegate(UnitManager unit);
		event CollisionDelegate collided;

		public void AddCollisionCallback(CollisionDelegate callback){
			collided += callback;
		}

		void OnCollisionEnter(Collision collision){
			if (collided != null) {
				collided (this);
			}
		}
	}
}
