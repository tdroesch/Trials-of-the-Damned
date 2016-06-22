using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

namespace OmegaFramework
{
	[CustomPropertyDrawer(typeof(Ability))]
	public class AbilityDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty abilityNameProp = property.FindPropertyRelative("abilityName");
			SerializedProperty buttonNameProp = property.FindPropertyRelative("buttonName");
			SerializedProperty iconProp = property.FindPropertyRelative("icon");
			SerializedProperty disabledIconProp = property.FindPropertyRelative("disabledIcon");
			SerializedProperty rangeProp = property.FindPropertyRelative("range");
			SerializedProperty cooldownProp = property.FindPropertyRelative("cooldownTime");
			SerializedProperty disableFlagsProp = property.FindPropertyRelative("disableFlags");
			SerializedProperty windUpProp = property.FindPropertyRelative("windup");
			SerializedProperty mainProp = property.FindPropertyRelative("main");
			SerializedProperty windDownProp = property.FindPropertyRelative("winddown");
			SerializedProperty losProp = property.FindPropertyRelative ("requiresLOS");

			string[] inputButtons = EditorUtilities.GetInputButtons();
			int buttonIndex = 0;
			for (int i = 0; i < inputButtons.Length; i++)
			{
				if (inputButtons[i] == buttonNameProp.stringValue)
				{
					buttonIndex = i;
					break;
				}
			}

			position.height = 16;
			EditorGUI.PropertyField(position, property, new GUIContent(abilityNameProp.stringValue));
			position.y += 18;
			if (property.isExpanded)
			{
				EditorGUI.indentLevel++;
				EditorUtilities.DrawProperty(ref position, abilityNameProp);
				buttonIndex = EditorGUI.Popup(position, buttonNameProp.displayName, buttonIndex, inputButtons);
				buttonNameProp.stringValue = inputButtons[buttonIndex];
				position.y += 18;
				EditorUtilities.DrawProperty(ref position, iconProp);
				EditorUtilities.DrawProperty(ref position, disabledIconProp);
				EditorUtilities.DrawProperty(ref position, rangeProp);
				EditorUtilities.DrawProperty(ref position, cooldownProp);
				EditorUtilities.DrawFlags(ref position, disableFlagsProp.displayName, disableFlagsProp);
				EditorUtilities.DrawProperty(ref position, windUpProp);
				EditorUtilities.DrawProperty(ref position, mainProp);
				EditorUtilities.DrawProperty(ref position, windDownProp);
				EditorUtilities.DrawProperty (ref position, losProp);
				EditorGUI.indentLevel--; 
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			float height = 18;
			if (property.isExpanded)
			{
				height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("abilityName")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("buttonName")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("icon")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("disabledIcon")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("range")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("cooldownTime")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("disableFlags")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("windup")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("main")) +
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("winddown")) + 
					EditorGUI.GetPropertyHeight(property.FindPropertyRelative("requiresLOS")) + 20;
			}
			return height;
		}
	}
}
