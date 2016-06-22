using UnityEngine;
using System.Collections;
using OmegaFramework;

public class RingOutKill : MonoBehaviour {
	public UnitManager playerUM;
	void OnTriggerEnter(Collider other){
		UnitManager otherUM = other.GetComponent<UnitManager>();
		if (otherUM != null){
			otherUM.AddDamage(playerUM, float.MaxValue);
		}
		else {
			Destroy(other.gameObject);
		}
	}
}
