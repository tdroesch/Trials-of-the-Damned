/*
 * SearchArea.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace OmegaFramework
{
	/// <summary>
	/// Search an area and apply the effect.
	/// </summary>
	public class SearchArea : Effect
	{
		float arc;
		float arcOffset;
		float radius;
		List<SerializableEffect> effects;

		public SearchArea(float arc, float arcOffset, float radius, List<SerializableEffect> effects)
		{
			this.arc = arc;
			this.arcOffset = arcOffset;
			this.radius = radius;
			this.effects = effects;
		}

		public float Arc
		{
			get {return arc;}
		}

		public float ArcOffset
		{
			get {return arcOffset;}
		}

		public float Radius
		{
			get {return radius;}
		}

		public List<SerializableEffect> Effects
		{
			get {return effects;}
		}

		public override void Execute (UnitManager source, UnitManager target, Vector3 position)
		{
			/* Execute all effects on every unitmanager found in the area
			 * the center of the arc is determined by the staight line from the target to the position
			 * If target is null the search will be centered at position with the arc centered 
			 * at the straight line from source to position.
			 * If target is not null then the search will be centered at target with the arc
			 * center facing position.
			 */
	
			//Get the center of the area
			Vector3 center;
			Vector3 arcCenter;
			if (target == null) {
				center = position;
				arcCenter = Quaternion.AngleAxis (arcOffset, Vector3.up) * (position - source.transform.position).normalized;
			} else {
				center = target.transform.position;
				arcCenter = Quaternion.AngleAxis (arcOffset, Vector3.up) * (position - target.transform.position).normalized;
			}
			int targetlayer = source.tag == "Player" ? RuntimeUtilities.ENEMY_LAYER : RuntimeUtilities.PLAYER_LAYER;
			Collider[] targets = Physics.OverlapSphere (center, radius, targetlayer);
			foreach (Collider hit in targets) {
				UnitManager hitTarget = hit.GetComponent<UnitManager> ();
				if (hitTarget != null) {
					Vector3 hitPosition = hit.transform.position - center;
					float angle = Vector3.Angle (arcCenter, hitPosition);
					if (Mathf.Abs (angle) <= arc / 2) {
						foreach (SerializableEffect effect in effects) {
							effect.Execute (source, hitTarget, center);
						}
					} 
				}
			}
		}
	}
}
