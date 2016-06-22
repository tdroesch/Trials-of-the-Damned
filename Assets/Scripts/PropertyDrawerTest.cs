using UnityEngine;
using System.Collections;

public class PropertyDrawerTest : MonoBehaviour {

	[SerializeField]
	public TestProperty testProperty;
	[SerializeField]
	public TestProperty testProperty2;
	[SerializeField]
	public TestProperty testProperty3;
	//[SerializeField]
	//public TestProperty testProperty4;
	//[SerializeField]
	//public TestProperty testProperty5;
	//[SerializeField]
	//public TestProperty testProperty6;
	public int nInt;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

[System.Serializable]
public class TestProperty
{
	[SerializeField]
	public InnerTestProperty innerProperty;
	//public Vector3[] vecArray;
}

[System.Serializable]
public class InnerTestProperty
{
	public float nFloat;
	public int nInt;
	public Vector3 vec;
	//public string[] array;
}