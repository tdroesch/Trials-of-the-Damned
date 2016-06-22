using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace OmegaFramework
{
	public class UIIcon : MonoBehaviour
	{
		[SerializeField]
		Image icon;
		[SerializeField]
		Text cooldown;

		public void UpdateIcon (Sprite icon, float cooldown)
		{
			this.icon.sprite = icon;
			this.cooldown.text = Mathf.Ceil(cooldown).ToString ();
			this.cooldown.enabled = cooldown > 0;
		}
	}
}
