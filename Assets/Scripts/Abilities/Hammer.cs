//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class Hammer : Ability
//{
//	public float arc = 75.0f;
//
////	protected override IEnumerator ExecuteAbility (float d, float c, Vector3 mousePos, UnitManager source)
////	{
//////		cooldown = cooldownTime / c;
//////		yield return new WaitForSeconds (this.windUp);
//////		
//////		if ((source.GetUnit ().flags & (Unit.DEAD_FLAG | Unit.SILENCED_FLAG | Unit.STUNNED_FLAG)) == 0) {
//////			Damage damage = new Damage (d * this.damageRatio);
//////			List<Effect> saEffects = new List<Effect> ();
//////			saEffects.Add (damage);
//////			SearchArea slash = new SearchArea (arc, range, transform.position, saEffects);
//////		
//////			slash.Execute (source, source.gameObject);
//////			spawnParticle();
//////			playSoundFX();
////		//		}
////		yield return null;
////	}
//	/*public float arc = 75.0f;
//		public GameObject effect;
//
//		// Use this for initialization
//		void Start () {
//			cooldownTime = 0;
//		}
//
//		// Update is called once per frame
//		void Update ()
//		{
//				if (cooldownTime > 0) {
//						cooldownTime -= Time.deltaTime;
//						if (cooldownTime < 0) {
//								cooldownTime = 0;
//						}
//				}
//				if (execute) {
//						time += Time.deltaTime;
//						if (time >= castTime) {
//								Damage (dmg);
//								execute = false;
//								time = 0;
//						}
//				}
//		}
//
//		public override void Execute (float dmg)
//		{
//				this.dmg = dmg;
//				execute = true;
//				time = castTime;
//				cooldownTime = cooldown - castTime;
//				{
//						Vector3 startPos = transform.TransformPoint (new Vector3 (2.8f, 5.6f, 5.9f));
//						Quaternion startRot = Quaternion.Euler (transform.rotation.eulerAngles + new Vector3(0f ,0f ,0f));
//						GameObject eff = Instantiate (effect, startPos, startRot) as GameObject;
//						eff.transform.parent = this.transform;
//						Destroy (eff, actionTime-castTime);
//				}
//		}
//
//		protected override void Damage (float dmg)
//		{
//				Collider[] targets;
//				if (tag == "Player") {
//						targets = Physics.OverlapSphere (transform.position, range, 1 << 10);
//				} else {
//						targets = Physics.OverlapSphere (transform.position, range, 1 << 11);
//				}
//				foreach (Collider target in targets) {			
//						Vector3 position = target.gameObject.transform.position - transform.position;
//						float angle = Vector3.Angle (transform.forward, position);
//						if (Mathf.Abs (angle) <= arc / 2) {
//								target.gameObject.GetComponent<Unit> ().DamageUnit (dmg * damageRatio);
//						}
//				}
//		}
//		*/
//}
