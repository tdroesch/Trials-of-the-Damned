//using UnityEngine;
//using System.Collections.Generic;
//using System.Collections;
//
//public class Charge : Movement
//{
//	SearchArea effect;
//
//	public void Init (UnitManager source, Vector3 destination, float duration, Effect effect)
//	{
//		base.SetDestination(source, destination, duration);
//		this.effect = (SearchArea)effect;
//	}
//
//	// Update is called once per frame
//	protected override void Update ()
//	{
//		base.Update();
//		effect.Center = transform.position;
//		effect.Execute (source);
//	}
//	
//	protected override void changeStats (){
//		target.AddFlags (Unit.MOVE_BLOCK_FLAG);
//		TakeNavAgent();
//	}
//	
//	protected override void revertStats (){
//		target.RemoveFlags (Unit.MOVE_BLOCK_FLAG);
//		ReleaseNavAgent();
//	}
//}