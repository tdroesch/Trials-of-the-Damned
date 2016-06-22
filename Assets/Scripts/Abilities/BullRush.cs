//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class BullRush : Ability
//{
//	private Vector3 destination;
//	float duration;
//	public float knockbackDistance = 5.0f;
//	public float knockbackDuration = 0.2f;
//
////	protected override IEnumerator ExecuteAbility (float d, float c, Vector3 mousePos, UnitManager source)
////	{
//////		duration = animationTime - windUp;
//////		cooldown = cooldownTime / c;
//////		yield return new WaitForSeconds (this.windUp);
//////
//////		if ((source.GetUnit ().flags & (Unit.DEAD_FLAG | Unit.SILENCED_FLAG | Unit.STUNNED_FLAG)) == 0) {
//////			Damage damage = new Damage (d * this.damageRatio);
//////			ApplyForce force = new ApplyForce ("ForcedMovement", knockbackDuration, knockbackDistance);
//////			List<Effect> saEffects = new List<Effect> ();
//////			saEffects.Add (damage);
//////			saEffects.Add (force);
//////			SearchArea sa = new SearchArea (360.0f, knockbackDistance, transform.position, saEffects);
//////
//////			destination = ((mousePos - transform.position).normalized * this.range) + transform.position;
//////			ApplyForce charge = new ApplyForce ("Charge", duration, destination, sa);
//////
//////			charge.Execute (source, source.gameObject);
//////			spawnParticle();
//////			playSoundFX();
//////		}
////		yield return null;
////	}
//
//	/*public override void Execute (float dmg)
//	{
//		this.dmg = dmg;
//		GetDestination ();
//		execute = true;
//		time = castTime;
//		cooldownTime = cooldown - castTime;
//		GetComponent<AudioSource> ().PlayOneShot (GetComponent<Sound_Script> ().AudioList[3]);
//		{
//			Vector3 startPos = transform.TransformPoint (new Vector3 (0, 7, 9));
//			transform.LookAt (destination);
//			GameObject eff = Instantiate (lionEffect, startPos, transform.rotation) as GameObject;
//			eff.transform.parent = this.transform;
//			Destroy (eff, actionTime-castTime);
//		}
//	}
//
//	protected override void Damage (float dmg)
//	{
//		Collider[] targets = Physics.OverlapSphere (transform.position + new Vector3(0f,3.5f), knockback, 1 << 10);
//		foreach (Collider target in targets) {
//			Knockback (target.gameObject, knockback);
//			Unit unit = target.GetComponent<Unit> ();
//			if (unit != null && unit.state != UnitState.FLINCHING)
//				unit.DamageUnit (dmg * damageRatio);
//		}
//	}*/
//
//	/*void Knockback (GameObject target, float dist){
//		ForcedMovement fm = target.GetComponent<ForcedMovement> ();
//		if (fm == null) {
//			fm = target.AddComponent<ForcedMovement> ();
//		}
//		fm.SetMotion (transform.position, dist, 0.2f);
//	}*/
//
//	/*void Charge (Vector3 dest, float speed){
//		transform.LookAt(dest);
//		controller.Move ((dest-transform.position).normalized * speed * Time.deltaTime);
//		Damage (dmg);
//	}
//
//	void GetDestination () {
//		Ray target = Camera.main.ScreenPointToRay (Input.mousePosition);
//		RaycastHit hit = new RaycastHit();
//		if (Physics.Raycast (target, out hit, Mathf.Infinity, (1 << 9)|(1 << 10))){
//			if (hit.collider.gameObject.tag == "Unit") {
//				destination = hit.collider.transform.position;
//			}
//			else if (hit.collider.gameObject.layer == 9) {
//				destination = hit.point;
//			}
//			destination.y = transform.position.y;
//			destination = ((destination - transform.position).normalized * this.range) + transform.position;
//		}
//	}*/
//}
