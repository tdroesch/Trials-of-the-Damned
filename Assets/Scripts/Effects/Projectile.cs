/*
 * Projectile.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OmegaFramework
{
	public class Projectile : MonoBehaviour
	{
		Vector3 motion;
		[SerializeField]
		List<SerializableEffect> effects;
		UnitManager source;
		[SerializeField]
		GameObject onDestroySpawn;
		[SerializeField]
		bool destroyOnCollison;

		public void Init (float speed, float duration, UnitManager source, Vector3 position)
		{
			this.motion = speed * (position - transform.position).normalized;
			GameObject.Destroy (gameObject, duration);
			this.source = source;
		}

		void FixedUpdate ()
		{
			transform.position += motion * Time.fixedDeltaTime;
		}

		void OnTriggerEnter (Collider collider)
		{
			if ((1 << collider.gameObject.layer == RuntimeUtilities.PLAYER_LAYER && 1 << source.gameObject.layer == RuntimeUtilities.ENEMY_LAYER) ||
			    (1 << collider.gameObject.layer == RuntimeUtilities.ENEMY_LAYER && 1 << source.gameObject.layer == RuntimeUtilities.PLAYER_LAYER)) {
				UnitManager target = collider.GetComponent<UnitManager> ();
				foreach (SerializableEffect effect in effects) {
					effect.Execute (source, target, target.transform.forward + target.transform.position);
				}
				if (destroyOnCollison) {
					GameObject.Destroy (gameObject); 
				}
			}
		}

		void OnDestroy ()
		{
			if (onDestroySpawn != null) {
				GameObject.Instantiate (onDestroySpawn, this.transform.position, new Quaternion ());
			}
		}
	}
}
