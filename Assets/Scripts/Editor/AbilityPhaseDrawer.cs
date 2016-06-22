using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

namespace OmegaFramework
{
	[CustomPropertyDrawer(typeof(Ability.AbilityPhase))]
	public class AbilityPhaseDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
			SerializedProperty selfEffectsProp = property.FindPropertyRelative ("selfEffects");
			SerializedProperty targetEffectsProp = property.FindPropertyRelative ("targetEffects");
			SerializedProperty animationProp = property.FindPropertyRelative ("animation");
			SerializedProperty vfxProp = property.FindPropertyRelative ("vfx");
			SerializedProperty vfxPositionProp = property.FindPropertyRelative ("vfxPosition");
			SerializedProperty vfxRotationProp = property.FindPropertyRelative ("vfxRotation");
			SerializedProperty sfxProp = property.FindPropertyRelative ("sfx");
			SerializedProperty volumeProp = property.FindPropertyRelative ("volume");
			SerializedProperty setFlagsProp = property.FindPropertyRelative ("setFlags");
			SerializedProperty cancelFlagsProp = property.FindPropertyRelative ("cancelFlags");
			SerializedProperty automaticProp = property.FindPropertyRelative ("automatic");
			SerializedProperty endOnCollisionProp = property.FindPropertyRelative ("endOnCollision");
			string[] animatorTriggers = EditorUtilities.GetAnimatorParams(((UnitManager)property.serializedObject.targetObject).GetComponent<Animator>(), AnimatorControllerParameterType.Trigger);
			if (animatorTriggers.Length == 0)
			{
				Debug.LogWarning("UnitManagerEditor could not find any Animator Triggers.  Either there are not trigger parameters in the animator it it needs to be refreshed in the inspector.");
			}
			int abilityTriggerIndex = 0;
			for (int i = 0; i < animatorTriggers.Length; i++)
			{
				if (animationProp.stringValue == animatorTriggers[i])
				{
					abilityTriggerIndex = i;
				}
			}

			position.height = 16;
			EditorGUI.PropertyField(position, property, label);
			position.y += 18;
			if (property.isExpanded)
			{
				EditorGUI.indentLevel++;
				EditorUtilities.DrawList(ref position, selfEffectsProp);
				EditorUtilities.DrawList(ref position, targetEffectsProp);
				position.height = 16;
				if (animatorTriggers.Length > 0)
				{
					abilityTriggerIndex = EditorGUI.Popup(position, animationProp.displayName, abilityTriggerIndex, animatorTriggers);
					animationProp.stringValue = abilityTriggerIndex == 0 ? null : animatorTriggers[abilityTriggerIndex];
				}
				else {
					EditorGUI.Popup(position, animationProp.displayName, 0, new string[] { "ERROR" });
				}
				position.y += 18;
				EditorUtilities.DrawProperty(ref position, vfxProp);
				EditorUtilities.DrawProperty(ref position, vfxPositionProp);
				EditorUtilities.DrawProperty(ref position, vfxRotationProp);
				EditorUtilities.DrawProperty(ref position, sfxProp);
				EditorUtilities.DrawProperty(ref position, volumeProp);
				EditorUtilities.DrawFlags(ref position, setFlagsProp.displayName, setFlagsProp);
				EditorUtilities.DrawFlags(ref position, cancelFlagsProp.displayName, cancelFlagsProp);
				EditorUtilities.DrawProperty(ref position, automaticProp);
				EditorUtilities.DrawProperty (ref position, endOnCollisionProp);
				EditorGUI.indentLevel--;
			}
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label){
			float height = 18;
			if (property.isExpanded)
			{
				height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("selfEffects")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("targetEffects")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("animation")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("vfx")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("vfxPosition")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("vfxRotation")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("sfx")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("volume")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("setFlags")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("cancelFlags")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("automatic")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative ("endOnCollision")) + 22;
			}
			return height;
		}
	}
}
