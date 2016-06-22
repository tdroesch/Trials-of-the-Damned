using UnityEngine;
using UnityEditor;
using System.Collections;

namespace OmegaFramework
{
	[CustomEditor(typeof(UnitManager))]
	public class UnitManagerEditor : Editor
	{
		SerializedProperty speedProp;
		SerializedProperty damageProp;
		SerializedProperty cooldownProp;
		SerializedProperty healthProp;
		SerializedProperty regenThreshProp;
		SerializedProperty regenDelayProp;
		SerializedProperty regenRateProp;
		SerializedProperty flinchDurationProp;
		SerializedProperty killScoreProp;
		SerializedProperty flagsProp;
		SerializedProperty deathTriggerProp;
		SerializedProperty flinchTriggerProp;
		SerializedProperty movingBoolProp;
		SerializedProperty despawnTimeProp;
		int deathTriggerIndex;
		int flinchTriggerIndex;
		int movingBoolIndex;

		SerializedProperty abilitiesProp;
		SerializedProperty buffsProp;

		void OnEnable(){
			speedProp = serializedObject.FindProperty ("unit.baseStats.moveSpeed");
			damageProp = serializedObject.FindProperty ("unit.baseStats.damage");
			cooldownProp = serializedObject.FindProperty ("unit.baseStats.cooldownRate");
			healthProp = serializedObject.FindProperty ("unit.baseStats.maxHealth");
			regenThreshProp = serializedObject.FindProperty ("unit.baseStats.regenThresh");
			regenDelayProp = serializedObject.FindProperty ("unit.baseStats.regenDelay");
			regenRateProp = serializedObject.FindProperty ("unit.baseStats.regenRate");
			flinchDurationProp = serializedObject.FindProperty ("unit.baseStats.flinchDuration");
			killScoreProp = serializedObject.FindProperty ("unit.baseStats.killScore");
			flagsProp = serializedObject.FindProperty ("unit.baseFlags.flags");
			despawnTimeProp = serializedObject.FindProperty ("despawnTime");
			deathTriggerProp = serializedObject.FindProperty ("deathTrigger");
			flinchTriggerProp = serializedObject.FindProperty ("flinchTrigger");
			movingBoolProp = serializedObject.FindProperty ("movingBool");
			abilitiesProp = serializedObject.FindProperty ("abilities");
			buffsProp = serializedObject.FindProperty ("buffs");
			string[] animatorTriggers = EditorUtilities.GetAnimatorParams (((UnitManager)target).GetComponent<Animator> (), AnimatorControllerParameterType.Trigger);
			for (int i = 0; i < animatorTriggers.Length; i++) {
				if (deathTriggerProp.stringValue == animatorTriggers [i]) {
					deathTriggerIndex = i;
				}
				if (flinchTriggerProp.stringValue == animatorTriggers [i]) {
					flinchTriggerIndex = i;
				}
			}
			string[] animatorBools = EditorUtilities.GetAnimatorParams (((UnitManager)target).GetComponent<Animator> (), AnimatorControllerParameterType.Bool);
			for (int i = 0; i < animatorBools.Length; i++) {
				if (movingBoolProp.stringValue == animatorBools [i]) {
					movingBoolIndex = i;
				}
			}
		}

		public override void OnInspectorGUI ()
		{
			serializedObject.Update ();

			string[] animatorTriggers = EditorUtilities.GetAnimatorParams (((UnitManager)target).GetComponent<Animator> (), AnimatorControllerParameterType.Trigger);
			if (animatorTriggers.Length == 0) {
				Debug.LogWarning ("UnitManagerEditor could not find any Animator Triggers.  Either there are no trigger parameters in the animator it it needs to be refreshed in the inspector.");
			}
			string[] animatorBools = EditorUtilities.GetAnimatorParams (((UnitManager)target).GetComponent<Animator> (), AnimatorControllerParameterType.Bool);
			if (animatorBools.Length == 0) {
				Debug.LogWarning ("UnitManagerEditor could not find any Animator Bools.  Either there are no bool parameters in the animator it it needs to be refreshed in the inspector.");
			}

			EditorGUILayout.PropertyField (speedProp, new GUIContent("Speed"));
			EditorGUILayout.PropertyField (damageProp, new GUIContent("Damage"));
			EditorGUILayout.PropertyField (cooldownProp, new GUIContent("Cool Down Rate"));
			EditorGUILayout.PropertyField (healthProp, new GUIContent("Health"));
			EditorGUILayout.PropertyField (regenThreshProp, new GUIContent("Regen Threshhold"));
			EditorGUILayout.PropertyField (regenDelayProp, new GUIContent("Regen Delay"));
			EditorGUILayout.PropertyField (regenRateProp, new GUIContent("Regen Rate"));
			EditorGUILayout.PropertyField (flinchDurationProp, new GUIContent("Flinch Duration"));
			EditorGUILayout.PropertyField (killScoreProp, new GUIContent("Kill Score"));
			EditorGUILayout.PropertyField (despawnTimeProp, new GUIContent ("Despawn Time"));
			flagsProp.intValue = EditorGUILayout.MaskField ("Flags", flagsProp.intValue, EditorUtilities.flagOptions);
			if (animatorTriggers.Length > 1)
			{
				if (deathTriggerIndex < 0 || deathTriggerIndex >= animatorTriggers.Length)
				{
					deathTriggerIndex = 0;
				}
				deathTriggerIndex = EditorGUILayout.Popup ("Death Trigger",deathTriggerIndex, animatorTriggers);
				deathTriggerProp.stringValue = deathTriggerIndex == 0 ? null : animatorTriggers [deathTriggerIndex];
				if (flinchTriggerIndex < 0 || flinchTriggerIndex >= animatorTriggers.Length)
				{
					flinchTriggerIndex = 0;
				}
				flinchTriggerIndex = EditorGUILayout.Popup ("Flinch Trigger",flinchTriggerIndex, animatorTriggers);
				flinchTriggerProp.stringValue = flinchTriggerIndex == 0 ? null : animatorTriggers [flinchTriggerIndex];
			} else {
				EditorGUILayout.Popup ("Death Trigger",0, new string[] { "ERROR" });
				EditorGUILayout.Popup ("Flinch Trigger",0, new string[] { "ERROR" });
			}
			if (animatorBools.Length > 1)
			{
				if (movingBoolIndex < 0 || movingBoolIndex >= animatorBools.Length)
				{
					movingBoolIndex = 0;
				}
				movingBoolIndex = EditorGUILayout.Popup ("Moving Bool",movingBoolIndex, animatorBools);
				movingBoolProp.stringValue = movingBoolIndex == 0 ? null : animatorBools [movingBoolIndex];
			} else {
				EditorGUILayout.Popup ("Moving Bool",0, new string[] { "ERROR" });
			}
			EditorUtilities.DrawListLayout (abilitiesProp);
			EditorUtilities.DrawListLayout (buffsProp);

			serializedObject.ApplyModifiedProperties ();
		}
	}
}
