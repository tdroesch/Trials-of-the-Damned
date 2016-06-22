using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace OmegaFramework
{
	public class UINumber : MonoBehaviour
	{
		Text text;
		Color color;
		[SerializeField]
		float initialValue;
		float value;
		[SerializeField]
		string prefix;
		[SerializeField]
		string suffix;
		[SerializeField]
		Color flashColor;
		[SerializeField]
		float flashDuration;
		[SerializeField]
		float displayDuration;
		//If negative value, is always displayed
		[SerializeField]
		float fadeDuration;
		float timeSinceUpdate;

		public float Value {
			get {
				return value;
			}
		}
	
		// Use this for initialization
		void Awake ()
		{
			text = GetComponent<Text> ();
			color = text.color;
			ResetValue ();
		}
		
		// Update is called once per frame
		void Update ()
		{
			timeSinceUpdate += Time.deltaTime;
			if (timeSinceUpdate <= flashDuration) {
				text.color = Color.Lerp (color, flashColor, Mathf.PingPong (timeSinceUpdate, flashDuration / 2) / (flashDuration / 2));
			} else {
				text.color = color;
			}
			Color textColor = text.color;
			textColor.a = displayDuration < 0 ? 1 : Mathf.Lerp (1, 0, (timeSinceUpdate - displayDuration) / fadeDuration);
			text.color = textColor;
		}

		public void ResetValue ()
		{
			value = initialValue;
			timeSinceUpdate = displayDuration + fadeDuration;
			text.text = prefix + value.ToString() + suffix;
			Color temp = color;
			temp.a = displayDuration < 0 ? 1 : 0;
			text.color = temp;
		}

		public void AddValue (float value)
		{
			this.value += value;
			timeSinceUpdate = 0;
			text.text = prefix + this.value.ToString() + suffix;
		}

		public void SetValue (float value)
		{
			this.value = value;
			timeSinceUpdate = 0;
			text.text = prefix + this.value.ToString() + suffix;
		}
	}
}