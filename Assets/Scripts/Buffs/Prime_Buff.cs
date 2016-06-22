//using UnityEngine;
//using System.Collections;
//
//public class Prime_Buff : Buff {
//	float speed;
//	float cdr;
//	float damage;
//	float regen;
//	float regenThreshhold;
//	float regenDelay;
//
//	public override void Init (UnitManager source, float duration, Hashtable data)
//	{
//		this.speed = 1+(float)data["Speed"];
//		this.damage = 1+(float)data["Damage"];
//		this.cdr = 1+(float)data["CDR"];
//		this.regen = 1 + (float)data["Regen"];
//		this.regenThreshhold = 130;
//		this.regenDelay = -5;
//		base.Init (source, duration, data);
//	}
//
//	protected override void changeStats (){
//		source.ChangeStatsPercent (speed, damage, cdr, 1, 1, regen);
//		source.ChangeStatsFlat (0, 0, 0, regenThreshhold, regenDelay, 0);
//		source.AddFlags (Unit.CC_IMMUNE_FLAG);
//		//source.SetAnimation ("Prime", true);
//	}
//	
//	protected override void revertStats (){
//		source.ChangeStatsPercent (1/speed, 1/damage, 1/cdr, 1, 1, 1/regen);
//		source.ChangeStatsFlat (0, 0, 0, -1*regenThreshhold, -1*regenDelay, 0);
//		source.RemoveFlags (Unit.CC_IMMUNE_FLAG);
//		//source.SetAnimation ("Prime", false);
//	}
//}