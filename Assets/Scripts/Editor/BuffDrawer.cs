using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

namespace OmegaFramework
{
	[CustomPropertyDrawer(typeof(Buff))]
	public class BuffDrawer : PropertyDrawer
	{

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty durationProp = property.FindPropertyRelative("totalDuration");
			SerializedProperty periodProp = property.FindPropertyRelative("period");
			SerializedProperty delayProp = property.FindPropertyRelative("delay");
			SerializedProperty periodicEffectsProp = property.FindPropertyRelative("periodicEffects");
			SerializedProperty flatStatsProp = property.FindPropertyRelative("flatStats");
			SerializedProperty percentStatsProp = property.FindPropertyRelative("percentStats");
			SerializedProperty flagsProp = property.FindPropertyRelative("flags.flags");
			SerializedProperty animationBoolProp = property.FindPropertyRelative ("animationBool");
			Animator anim = ((Component)property.serializedObject.targetObject).GetComponent<Animator> ();
			string[] animatorBools;
			if (anim != null) {
				animatorBools = EditorUtilities.GetAnimatorParams (anim, AnimatorControllerParameterType.Bool);
			} else {
				animatorBools = new string[0];
			}
			if (animatorBools.Length == 0) {
				Debug.LogWarning ("BuffDrawer could not find any Animator Bools.  Either there are no bool parameters in the animator it it needs to be refreshed in the inspector.");
			}
			int animationBoolIndex = 0;
			for (int i = 0; i < animatorBools.Length; i++) {
				if (animationBoolProp.stringValue == animatorBools [i]) {
					animationBoolIndex = i;
				}
			}

			position.height = 16;
			EditorGUI.PropertyField(position, property, label);
			position.y += 18;
			if (property.isExpanded)
			{
				EditorGUI.indentLevel++;
				EditorUtilities.DrawProperty(ref position, durationProp, new GUIContent("Duration"));
				EditorUtilities.DrawProperty(ref position, periodProp);
				EditorUtilities.DrawProperty(ref position, delayProp);
				EditorUtilities.DrawList(ref position, periodicEffectsProp);
				EditorUtilities.DrawProperty(ref position, flatStatsProp, true);
				EditorUtilities.DrawProperty(ref position, percentStatsProp, true);
				EditorUtilities.DrawFlags(ref position, flagsProp.displayName, flagsProp);
				position.height = 16;
				if (animatorBools.Length > 0)
				{
					animationBoolIndex = EditorGUI.Popup(position, animationBoolProp.displayName, animationBoolIndex, animatorBools);
					animationBoolProp.stringValue = animationBoolIndex == 0 ? null : animatorBools[animationBoolIndex];
				}
				else {
					EditorGUI.Popup(position, animationBoolProp.displayName, 0, new string[] { "ERROR" });
				}
				position.y += 18;
				EditorGUI.indentLevel--;
			}
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			float height = 16;
			if (property.isExpanded)
			{
				height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("totalDuration")) +
				EditorGUI.GetPropertyHeight(property.FindPropertyRelative("period")) +
				EditorGUI.GetPropertyHeight(property.FindPropertyRelative("delay")) +
				EditorGUI.GetPropertyHeight(property.FindPropertyRelative("periodicEffects")) +
				EditorGUI.GetPropertyHeight(property.FindPropertyRelative("flatStats")) +
				EditorGUI.GetPropertyHeight(property.FindPropertyRelative("percentStats")) +
				EditorGUI.GetPropertyHeight(property.FindPropertyRelative("flags.flags")) + 
				EditorGUI.GetPropertyHeight(property.FindPropertyRelative ("animationBool")) + 14;
			}
			return height;
		}
	}
}
