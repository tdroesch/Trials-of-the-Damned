/*
 * Buff.cs is part of the ARPGFramework
 * Written by Tyler D. Roesch
 * March 16 2016 
 */

using UnityEngine;
using System.Collections.Generic;
using System;

namespace OmegaFramework
{
	/// <summary>
	/// Buff that gets applied to a UnitManager.
	/// </summary>
	[System.Serializable]
	public class Buff : ICloneable
	{
		UnitManager source;

		public UnitManager Source {
			get {
				return source;
			}
			set {
				source = value;
			}
		}

		float duration;
		[SerializeField]
		float totalDuration;

		[SerializeField]
		UnitStats flatStats;

		internal UnitStats FlatStats {
			get {
				return flatStats;
			}
		}

		[SerializeField]
		UnitStats percentStats;

		internal UnitStats PercentStats {
			get {
				return percentStats;
			}
		}

		[SerializeField]
		UnitFlags flags;

		internal UnitFlags Flags {
			get {
				return flags;
			}
		}

		[SerializeField]
		float period;
		[SerializeField]
		float delay;
		[SerializeField]
		List<SerializableEffect> periodicEffects;
		[SerializeField]
		string animationBool;

		internal string AnimationBool {
			get {
				return animationBool;
			}
		}

		Buff (UnitManager source, float totalDuration, UnitStats flatStats, UnitStats percentStats, UnitFlags flags, List<SerializableEffect> periodicEffects, float period, string animationBool)
		{
			this.source = source;
			this.totalDuration = totalDuration;
			this.duration = 0;
			this.flatStats = flatStats;
			this.percentStats = percentStats;
			this.flags = flags;
			this.periodicEffects = new List<SerializableEffect> (periodicEffects);
			this.period = period;
			this.animationBool = animationBool;
		}

		internal float Update (UnitManager target)
		{
			duration += Time.deltaTime;
			float n = Mathf.Floor ((duration - delay) / period);
			if ((period * n + delay) > (duration - Time.deltaTime) || (duration - Time.deltaTime) == 0) {
				//Trigger Effects
				foreach (SerializableEffect e in periodicEffects) {
					e.Execute (source, target, target.transform.forward + target.transform.position);
				}
			}
			return totalDuration - duration;
		}

		internal BuffInfo Info ()
		{
			return new BuffInfo (totalDuration - duration, source);
		}

		public object Clone ()
		{
			return new Buff (this.source, this.totalDuration, this.flatStats, this.percentStats, this.flags, this.periodicEffects, this.period, this.animationBool);
		}
	}

	/// <summary>
	/// Buff info.
	/// </summary>
	public class BuffInfo
	{
		public float duration;
		public UnitManager source;

		public BuffInfo (float duration, UnitManager source)
		{
			this.duration = duration;
			this.source = source;
		}
	}
}