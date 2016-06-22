using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(TestProperty))]
public class TestPropertyDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.PropertyField(position, property, label, true);
		Debug.Log("Property: " + property.name);
		Debug.Log("Property height: " + EditorGUI.GetPropertyHeight(property));
		Debug.Log("CountInProperty: " + property.CountInProperty());
		//Debug.Log("CountRemaining: " + property.CountRemaining());
		//Debug.Log("GetEndProperty: " + property.GetEndProperty().name);
		//Debug.Log("GetEnumerator: " + property.GetEnumerator());
		//Debug.Log("Next: " + property.Next(true));
		//Debug.Log("Property: " + property.name);
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return property.CountInProperty() * 18 - 2;
	}
}
