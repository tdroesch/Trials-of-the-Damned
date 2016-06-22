using UnityEngine;
using System.Collections;
using OmegaFramework;

public class Fire_Aura : MonoBehaviour {
	public float duration = 5.0f;
	public float damage = 5.0f;
	UnitManager source;

	// Use this for initialization
	void Start () {
		GameObject.Destroy (gameObject, duration);
	}

	public void Init (UnitManager source) {
		this.source = source;
	}

	void OnTriggerStay (Collider other) 
	{
		UnitManager um = other.gameObject.GetComponent<UnitManager> ();
		if (um != null) {
			um.AddDamage (this.source, this.damage);
		}
	}
}
