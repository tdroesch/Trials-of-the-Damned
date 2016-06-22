using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace OmegaFramework
{
	public class UIBar : MonoBehaviour
	{
		Image bar;
		[SerializeField]
		Color highHealthColor;
		[SerializeField]
		Color lowHealthColor;
		RectTransform rectTransform;
	
		// Use this for initialization
		void Start ()
		{
			bar = GetComponent<Image> ();
			rectTransform = GetComponent<RectTransform> ();
			bar.color = highHealthColor;
		}

		public void SetHealth (float percentHealth)
		{
			bar.color = Color.Lerp (lowHealthColor, highHealthColor, percentHealth);
			Vector3 scale = rectTransform.localScale;
			scale.x = percentHealth;
			rectTransform.localScale = scale;
			if (percentHealth <= 0) {
				bar.enabled = false;
			} else {
				bar.enabled = true;
			}
		}
	}
}
