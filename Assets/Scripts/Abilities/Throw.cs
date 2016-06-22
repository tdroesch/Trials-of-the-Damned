//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class Throw : Ability
//{
//	public Rigidbody projectile = null;
//	public float speed = 100.0f;
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
//////			lp.Execute (source);
//////			spawnParticle();
//////			playSoundFX();
////		//		}
////		yield return null;
////	}
//	/*public Rigidbody projectile;
//	public float speed;
//	public Vector3 offset;
//	private Controller controller;
//	private GameObject target;
//
//	// Use this for initialization
//	void Start () {
//		controller = GetComponent<Controller> ();
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
//		if (controller.target != null) {
//			target = controller.target;
//			execute = true;
//			time = castTime;
//			cooldownTime = cooldown - castTime;
//		}
//	}
//	
//	protected override void Damage (float dmg)
//	{
//		Vector3 startPos = transform.TransformPoint (offset);
//		transform.LookAt(target.transform.position);
//		Rigidbody hammer = Instantiate (projectile, startPos, transform.rotation) as Rigidbody;
//		hammer.velocity = (toXZ(target.transform.position) - toXZ(hammer.position)).normalized * speed;
//		Projectile p = hammer.gameObject.AddComponent<Projectile> ();
//		p.dmg = dmg * damageRatio;
//		target = null;
//		GameObject.Destroy (hammer.gameObject, range/speed);
//	}
//
//	// Transforms removes the Y component from a vector3
//	Vector3 toXZ (Vector3 input)
//	{
//		return new Vector3 (input.x, this.transform.position.y, input.z);
//	}
//*/
//}
