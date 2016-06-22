//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class Prime_Ability : Ability
//{
//	public float duration = 15.0f;
//	public float speed = 0.5f;
//	public float damage = 0.5f;
//	public float cooldownRate = 0.5f;
//	public float regenRate = 1.0f;
//
////	protected override IEnumerator ExecuteAbility (float d, float c, Vector3 mousePos, UnitManager source)
////	{
//////		cooldown = cooldownTime/c;
//////
//////		Hashtable data = new Hashtable();
//////		data.Add ("Speed", speed);
//////		data.Add ("Damage", damage);
//////		data.Add ("CDR", cooldownRate);
//////		data.Add ("Regen", regenRate);
//////
//////		ApplyBuff ab = new ApplyBuff("Prime", duration, data);
//////		ab.Execute (source, source.gameObject);
//////		spawnParticle ();
//////		playSoundFX();
////		//		yield return null;
////		yield return null;
////	}
//}
//
