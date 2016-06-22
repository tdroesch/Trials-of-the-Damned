using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

namespace OmegaFramework {
	[CustomPropertyDrawer(typeof(SerializableEffect))]
	public class EffectDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty type = property.FindPropertyRelative("type");
			int index = type.propertyType == SerializedPropertyType.Enum ? type.enumValueIndex : type.intValue;
			position.height = 16;
			EditorGUI.PropertyField(position, property, new GUIContent(SerializableEffect.GetEffectTypes()[index]));
			position.y += 18;
			if (property.isExpanded)
			{
				EditorGUI.indentLevel++;
				if (type.propertyType == SerializedPropertyType.Enum)
				{
					type.enumValueIndex = (int)(SerializableEffect.EffectType)EditorGUI.EnumPopup(position, type.displayName, (SerializableEffect.EffectType)index);
				}
				else
				{
					type.intValue = (int)(SerializableEffect.EffectType)EditorGUI.EnumPopup(position, type.displayName, (SerializableEffect.EffectType)index);
				}
				position.y += 18;
				if (index == (int)SerializableEffect.EffectType.None)
				{
					position.height = 32;
					EditorGUI.HelpBox(EditorGUI.IndentedRect(position), "Select an effect type to set it's properties.", MessageType.Info);
				}
				if (index == (int)SerializableEffect.EffectType.Move)
				{
					EditorUtilities.DrawProperty(ref position, property.FindPropertyRelative("distance"));
					EditorUtilities.DrawProperty(ref position, property.FindPropertyRelative("duration"));
					EditorUtilities.DrawProperty(ref position, property.FindPropertyRelative("pathfinding"));
				}
				if (index == (int)SerializableEffect.EffectType.Damage)
				{
					EditorUtilities.DrawProperty(ref position, property.FindPropertyRelative("damageRatio"));
				}
				if (index == (int)SerializableEffect.EffectType.Buff)
				{
					EditorUtilities.DrawProperty(ref position, property.FindPropertyRelative("buff"), true);
				}
				if (index == (int)SerializableEffect.EffectType.SearchArea)
				{
					EditorUtilities.DrawProperty(ref position, property.FindPropertyRelative("arc"));
					EditorUtilities.DrawProperty(ref position, property.FindPropertyRelative("arcOffset"));
					EditorUtilities.DrawProperty(ref position, property.FindPropertyRelative("radius"));
					EditorUtilities.DrawList(ref position, property.FindPropertyRelative("effects"));
				}
				if (index == (int)SerializableEffect.EffectType.LaunchProjectile)
				{
					EditorUtilities.DrawProperty(ref position, property.FindPropertyRelative("projectile"));
					EditorUtilities.DrawProperty(ref position, property.FindPropertyRelative("speed"));
					EditorUtilities.DrawProperty(ref position, property.FindPropertyRelative("duration"));
				}
				EditorGUI.indentLevel--;
			}
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			float height = 18;
			SerializedProperty type = property.FindPropertyRelative("type");
			int index = type.propertyType == SerializedPropertyType.Enum ? type.enumValueIndex : type.intValue;
			if (property.isExpanded)
			{
				height += EditorGUI.GetPropertyHeight(type);
				if (index == (int)SerializableEffect.EffectType.None)
				{
					height += 32;
				}
				if (index == (int)SerializableEffect.EffectType.Move)
				{
					height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("distance")) +
						EditorGUI.GetPropertyHeight(property.FindPropertyRelative("duration")) +
						EditorGUI.GetPropertyHeight(property.FindPropertyRelative("pathfinding")) + 4;
				}
				if (index == (int)SerializableEffect.EffectType.Damage)
				{
					height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("damageRatio"));
				}
				if (index == (int)SerializableEffect.EffectType.Buff)
				{
					height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("buff"));
				}
				if (index == (int)SerializableEffect.EffectType.SearchArea)
				{
					height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("arc")) +
						EditorGUI.GetPropertyHeight(property.FindPropertyRelative("arcOffset")) +
						EditorGUI.GetPropertyHeight(property.FindPropertyRelative("radius")) +
						EditorGUI.GetPropertyHeight(property.FindPropertyRelative("effects")) + 6;
				}
				if (index == (int)SerializableEffect.EffectType.LaunchProjectile)
				{
					height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("projectile")) +
						EditorGUI.GetPropertyHeight(property.FindPropertyRelative("speed")) +
						EditorGUI.GetPropertyHeight(property.FindPropertyRelative("duration")) + 4;
				}
			}
			return height;
		}
	}
}
