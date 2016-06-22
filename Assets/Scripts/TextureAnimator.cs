using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class TextureAnimator : MonoBehaviour {
	public List<Material> materials;
	public List<Texture> textures;

	/***********************************************
	 * 		THESE FUNCTIONS CAN BE CALLED IN THE
	 * 		ANIMATION EDITOR
	 **********************************************/
	public void setTexture(string s)
	{
		string[] parameters = s.Split(',');
		if (parameters.Length == 2) {
			int matIndex = int.Parse(parameters[0]);
			int textIndex = int.Parse(parameters[1]);
			if (matIndex >= 0 && matIndex < materials.Count && textIndex >= 0 && textIndex < textures.Count) {
				materials [matIndex].mainTexture = textures [textIndex];
			}
		}
	}
}
