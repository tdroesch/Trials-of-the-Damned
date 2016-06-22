using UnityEngine;
using System.Collections.Generic;
using System;

namespace OmegaFramework
{
	[System.Serializable]
	public class SerializableEffect : ISerializationCallbackReceiver
	{
		public enum EffectType { None, Move, Damage, Buff, SearchArea, LaunchProjectile}

		public static string[] GetEffectTypes()
		{
			return Enum.GetNames(typeof(EffectType));
		}

		public EffectType type;

		#region MoveFields
		public float distance;
		public float duration;
		public bool pathfinding;
		#endregion
		#region DamageFields
		public float damageRatio;
		#endregion
		#region BuffFields
		public Buff buff;
		#endregion
		#region ProjectileFields
		public Rigidbody projectile;
		public float speed;
		//Also uses duration from MoveFields region
		#endregion
		#region SearchAreaFields
		public float arc;
		public float arcOffset;
		public float radius;
		public List<SerializableEffect> effects;
		#endregion

		Effect effect;

		public void Execute (UnitManager source, UnitManager target, Vector3 position)
		{
			effect.Execute(source, target, position);
		}
	
		public void OnAfterDeserialize()
		{
			if (type == EffectType.Move)
			{
				effect = new MoveUnit(distance, duration, pathfinding);
			}
			else if (type == EffectType.Damage)
			{
				effect = new ApplyDamage(damageRatio);
			}
			else if (type == EffectType.Buff)
			{
				effect = new ApplyBuff(buff);
			}
			else if (type == EffectType.SearchArea)
			{
				effect = new SearchArea(arc, arcOffset, radius, effects);
			}
			else if (type == EffectType.LaunchProjectile)
			{
				effect = new LaunchProjectile(projectile, speed, duration);
			} else
			{
				effect = null;
			}
		}

		public void OnBeforeSerialize()
		{
			ResetFields();
			if (effect != null)
			{
				Type effectType = effect.GetType();
				if (effectType.Equals(typeof(MoveUnit)))
				{
					type = EffectType.Move;
					distance = ((MoveUnit)effect).Distance;
					duration = ((MoveUnit)effect).Duration;
					pathfinding = ((MoveUnit)effect).Pathfinding;
				}
				else if (effectType.Equals(typeof(ApplyDamage)))
				{
					type = EffectType.Damage;
					damageRatio = ((ApplyDamage)effect).DamageRatio;
				}
				else if (effectType.Equals(typeof(ApplyBuff)))
				{
					type = EffectType.Buff;
					buff = ((ApplyBuff)effect).Buff;
				}
				else if (effectType.Equals(typeof(SearchArea)))
				{
					type = EffectType.SearchArea;
					arc = ((SearchArea)effect).Arc;
					arcOffset = ((SearchArea)effect).ArcOffset;
					radius = ((SearchArea)effect).Radius;
					effects = ((SearchArea)effect).Effects;
				}
				else if (effectType.Equals(typeof(LaunchProjectile)))
				{
					type = EffectType.LaunchProjectile;
					projectile = ((LaunchProjectile)effect).Projectile;
					speed = ((LaunchProjectile)effect).Speed;
					duration = ((LaunchProjectile)effect).Duration;
				} 
			}
		}

		void ResetFields()
		{
			type = EffectType.None;
			distance = 0;
			duration = 0;
			pathfinding = false;
			damageRatio = 1;
			buff = null;
			projectile = null;
			speed = 0;
			arc = 0;
			arcOffset = 0;
			radius = 0;
			effects = new List<SerializableEffect>();
		}
	} 
}
