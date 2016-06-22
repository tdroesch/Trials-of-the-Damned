//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class Fire : Ability
//{
//	public Rigidbody projectile = null;
//	public float speed = 100.0f;
//	public GameObject fire = null;
//	
////	protected override IEnumerator ExecuteAbility (float d, float c, Vector3 mousePos, UnitManager source)
////	{
//////		cooldown = cooldownTime / c;
//////		yield return new WaitForSeconds (this.windUp);
//////		
//////		if ((source.GetUnit ().flags & (Unit.DEAD_FLAG | Unit.SILENCED_FLAG | Unit.STUNNED_FLAG)) == 0) {
//////			Damage damage = new Damage (d * this.damageRatio);
//////			List<Effect> lpEffects = new List<Effect>();
//////			lpEffects.Add (damage);
//////			LaunchProjectile lp = new LaunchProjectile(projectile, (mousePos+Utilities.GAME_HEIGHT - this.transform.position+Utilities.GAME_HEIGHT).normalized * this.speed, this.range/this.speed , lpEffects);
//////			
//////			lp.Execute (source, fire);
//////			spawnParticle();
//////			playSoundFX();
////		//		}
////		yield return null;
////	}
//}
