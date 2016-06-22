using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OmegaFramework
{
	/// <summary>
	/// Utility functions used by editor components of the OmegaFramework
	/// </summary>
	public static class EditorUtilities {

		private static GUIContent
				moveButtonContent = new GUIContent("\u21b4", "Move down"),
				duplicateButtonContent = new GUIContent("+", "Duplicate"),
				deleteButtonContent = new GUIContent("-", "Delete"),
				addButtonContent = new GUIContent("+", "Add element");

		public static string[] flagOptions = new string[] { "Dead", "Stunned", "Silenced", "Immobilized", "CC Immune", "Damage Immune", "Move Blocked", "Ability Blocked", "Can Flinch", "Moving" };

		/// <summary>
		/// Gets the input buttons from the input manager.
		/// </summary>
		/// <returns>Input axes that are type KeyOrMouseButton.</returns>
		public static string[] GetInputButtons (){
			var inputManager = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];

			SerializedObject obj = new SerializedObject(inputManager);

			SerializedProperty axisArray = obj.FindProperty("m_Axes");

			List<string> inputButtons = new List<string> ();

			for (int i = 0; i < axisArray.arraySize; i++) {
				var axis = axisArray.GetArrayElementAtIndex (i);
				if (axis.FindPropertyRelative ("type").intValue == 0) {
					inputButtons.Add (axis.FindPropertyRelative ("m_Name").stringValue);
				}
			}

			return inputButtons.ToArray ();
		}

		public static string[] GetAnimatorParams (Animator _anim){
			string[] paramNames = new string[_anim.parameterCount+1];
			paramNames[0] = "None";
			for (int i = 0; i < _anim.parameterCount; i++) {
				paramNames [i + 1] = _anim.parameters [i + 1].name;
			}
			return paramNames;
		}

		public static string[] GetAnimatorParams (Animator _anim, AnimatorControllerParameterType _t){
			List<string> paramList = new List<string> ();
			paramList.Add("None");
			for (int i = 0; i < _anim.parameterCount; i++) {
				if (_anim.parameters [i].type == _t) {
					paramList.Add (_anim.parameters [i].name);
				}
			}
			return paramList.ToArray ();
		}
		public static void DrawProperty(ref Rect position, SerializedProperty property, bool includeChildren = false)
		{
			position.height = EditorGUI.GetPropertyHeight(property);
			EditorGUI.PropertyField (position, property, includeChildren);
			position.y += EditorGUI.GetPropertyHeight(property) + 2;
		}
		public static void DrawProperty(ref Rect position, SerializedProperty property, GUIContent label, bool includeChildren = false)
		{
			position.height = EditorGUI.GetPropertyHeight(property);
			EditorGUI.PropertyField(position, property, label, includeChildren);
			position.y += EditorGUI.GetPropertyHeight(property) + 2;
		}
		public static void DrawFlags(ref Rect position, string label, SerializedProperty property)
		{
			position.height = 16;
			property.intValue = EditorGUI.MaskField(position, label, property.intValue, flagOptions);
			position.y += 18;
		}
		public static void DrawList(ref Rect position, SerializedProperty property){
			position.height = 16;
			EditorGUI.PropertyField (position, property);
			position.y += 18;
			EditorGUI.indentLevel++;
			if (property.isExpanded) {
				Rect buttonPosition;
				for (int i = 0; i < property.arraySize; i++) {
					buttonPosition = position;
					buttonPosition.height = 16;
					buttonPosition.width = 20;
					buttonPosition.x += position.width - 62;
					if (GUI.Button (buttonPosition, moveButtonContent, EditorStyles.miniButtonLeft)) {
						property.MoveArrayElement (i, i + 1);
					}
					buttonPosition.x += 20;
					if (GUI.Button (buttonPosition, duplicateButtonContent, EditorStyles.miniButtonMid)) {
						property.InsertArrayElementAtIndex (i);
					}
					buttonPosition.x += 20;
					if (GUI.Button (buttonPosition, deleteButtonContent, EditorStyles.miniButtonRight)) {
						int oldsize = property.arraySize;
						property.DeleteArrayElementAtIndex (i);
						if (oldsize == property.arraySize) {
							property.DeleteArrayElementAtIndex (i);
						}
					} else {
						DrawProperty (ref position, property.GetArrayElementAtIndex (i), true);
					}
				}
				buttonPosition = position;
				buttonPosition.height = 16;
				buttonPosition.width -= EditorGUI.indentLevel * 16;
				buttonPosition.x += EditorGUI.indentLevel * 16;
				if (GUI.Button (buttonPosition, addButtonContent, EditorStyles.miniButton)) {
					property.arraySize += 1;
				}
				position.y += 18;
			}
			EditorGUI.indentLevel--;
		}
		public static void DrawListLayout(SerializedProperty property){
			EditorGUILayout.PropertyField (property);
			EditorGUI.indentLevel++;
			if (property.isExpanded) {
				for (int i = 0; i < property.arraySize; i++) {
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.PropertyField (property.GetArrayElementAtIndex (i), true);
					if (GUILayout.Button (moveButtonContent, EditorStyles.miniButtonLeft, GUILayout.Width(20))) {
						property.MoveArrayElement (i, i + 1);
					}
					if (GUILayout.Button (duplicateButtonContent, EditorStyles.miniButtonMid, GUILayout.Width(20))) {
						property.InsertArrayElementAtIndex (i);
					}
					if (GUILayout.Button (deleteButtonContent, EditorStyles.miniButtonRight, GUILayout.Width(20))) {
						int oldsize = property.arraySize;
						property.DeleteArrayElementAtIndex (i);
						if (oldsize == property.arraySize) {
							property.DeleteArrayElementAtIndex (i);
						}
					}
					EditorGUILayout.EndHorizontal ();
				}
				if (GUILayout.Button (addButtonContent, EditorStyles.miniButton)) {
					property.arraySize += 1;
				}
			}
			EditorGUI.indentLevel--;
		}
	}
}

