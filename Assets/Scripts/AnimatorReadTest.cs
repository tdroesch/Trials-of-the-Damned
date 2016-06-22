using UnityEngine;
using System.Collections;

public class AnimatorReadTest : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		print ("Animator Read Test");
		Animator animator = GetComponent<Animator> ();
		foreach (AnimatorControllerParameter param in animator.parameters) {
			print (param.name);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
