using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class FaceChanger : MonoBehaviour {
	public Material face;
	public Material mouth;
//	public int currentFace = 0;
//	public int currentMouth = 0;

	public List<Texture> faceTextures;

	/***********************************************
	 * 		THESE FUNCTIONS CAN BE CALLED IN THE
	 * 		ANIMATION EDITOR
	 **********************************************/
	public void setFace(int i)
	{
		face.mainTexture = faceTextures [i];
	}

	public void setMouth(int i)
	{
		mouth.mainTexture = faceTextures [i];
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown(KeyCode.F))
//		{
//			setFace(++currentFace%faceTextures.Count);
//		}
//		if (Input.GetKeyDown (KeyCode.G)) {
//			setMouth(++currentMouth%faceTextures.Count);
//		}
	}
}
