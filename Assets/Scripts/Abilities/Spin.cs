//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class Spin : Ability
//{
//	public float knockbackDistance = 5.0f;
//	public float knockbackDuration = 0.2f;
//	
////	protected override IEnumerator ExecuteAbility (float d, float c, Vector3 mousePos, UnitManager source)
////	{
//////		cooldown = cooldownTime / c;
//////		yield return new WaitForSeconds (this.windUp);
//////		
//////		if ((source.GetUnit ().flags & (Unit.DEAD_FLAG | Unit.SILENCED_FLAG | Unit.STUNNED_FLAG)) == 0) {
//////			Damage damage = new Damage (d * this.damageRatio);
//////			ApplyForce force = new ApplyForce ("ForcedMovement", knockbackDuration, knockbackDistance);
//////			List<Effect> saEffects = new List<Effect> ();
//////			saEffects.Add (damage);
//////			saEffects.Add (force);
//////			SearchArea sa = new SearchArea (360.0f, range, transform.position, saEffects);
//////		
//////			sa.Execute (source, source.gameObject);
//////			spawnParticle();
//////			playSoundFX();
//////
////		//		}
////		yield return null;
////	}
//	/*public float knockback = 10.0f;
//	public GameObject effect;
//	public GameObject bindObject;
//
//	// Use this for initialization
//	void Start () {
//		cooldownTime = 0;
//	}
//
//	// Update is called once per frame
//	void Update () {
//		if (cooldownTime > 0) {
//			cooldownTime -= Time.deltaTime;
//			if (cooldownTime < 0) {
//				cooldownTime = 0;
//			}
//		}
//		if (execute) {
//			time += Time.deltaTime;
//			if (time >= castTime) {
//				Damage (dmg);
//				execute = false;
//				time = 0;
//			}
//		}
//	}
//
//	public override void Execute (float dmg)
//	{
//		this.dmg = dmg;
//		execute = true;
//		time = castTime;
//		cooldownTime = cooldown - castTime;
//		GetComponent<AudioSource> ().PlayOneShot (GetComponent<Sound_Script> ().AudioList[2]);
//		{
//			Vector3 startPos = bindObject.transform.TransformPoint (new Vector3 (-4f, 1.5f, -0.2f));
//			Quaternion startRot = Quaternion.Euler ( 322.763f, 27.43696f, 308.8744f);
//			GameObject eff = Instantiate (effect, startPos, startRot) as GameObject;
//			eff.transform.parent = bindObject.transform;
//			Destroy (eff, actionTime-castTime);
//		}
//	}
//	protected override void Damage (float dmg)
//	{
//		Collider[] targets = Physics.OverlapSphere (transform.position + new Vector3(0f,3.5f), range, 1 << 10);
//		foreach (Collider target in targets) {			
//			target.gameObject.GetComponent<Unit> ().DamageUnit (dmg * damageRatio);
//			Knockback (target.gameObject, knockback);
//		}
//	}
//	void Knockback (GameObject target, float dist){
//		ForcedMovement fm = target.AddComponent<ForcedMovement> ();
//		fm.SetMotion (transform.position, dist, 0.5f);
//	}
//	*/
//}
