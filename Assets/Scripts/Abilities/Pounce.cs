//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class Pounce : Ability
//{
//	public float knockbackDistance = 50.0f;
//	public float knockbackDuration = 0.5f;
//
////	protected override IEnumerator ExecuteAbility (float d, float c, Vector3 mousePos, UnitManager source)
////	{
//////		cooldown = cooldownTime / c;
//////		yield return new WaitForSeconds (this.windUp);
//////		
//////		if ((source.GetUnit ().flags & (Unit.DEAD_FLAG | Unit.SILENCED_FLAG | Unit.STUNNED_FLAG)) == 0) {
//////			Damage damage = new Damage (d * this.damageRatio);
//////			ApplyForce force = new ApplyForce ("ForcedMovement", knockbackDuration, knockbackDistance);
//////			List<Effect> saEffects = new List<Effect>();
//////			saEffects.Add (damage);
//////			saEffects.Add (force);
//////			SearchArea sa = new SearchArea(360.0f, range, this.transform.position, saEffects);
//////			
//////			sa.Execute (source);
//////			spawnParticle();
//////			playSoundFX();
////		//		}
////		yield return null;
////	}
//}
