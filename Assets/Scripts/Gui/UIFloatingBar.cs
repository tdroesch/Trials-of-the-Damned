using UnityEngine;
using System.Collections;

namespace OmegaFramework
{
	public class UIFloatingBar : MonoBehaviour
	{
		[SerializeField]
		UIBar bar;
		[SerializeField]
		UnitManager unit;

		// Use this for initialization
		void Start ()
		{
			AlignToCamera ();
		}
		
		// Update is called once per frame
		void LateUpdate ()
		{
			AlignToCamera ();
			bar.SetHealth (unit.Health / unit.Stats.maxHealth);
		}

		void AlignToCamera (){
			Vector3 lookAt = Camera.main.transform.position;
			lookAt.x = transform.position.x;
			transform.LookAt (lookAt);
		}
	}
}
