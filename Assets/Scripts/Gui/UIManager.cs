using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace OmegaFramework
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField]
		UnitManager player;
		[SerializeField]
		UIBar healthBar;
		[SerializeField]
		List<AbilityIcon> abilityIcons;
		[SerializeField]
		UINumber score;
		[SerializeField]
		UINumber killScore;
		[SerializeField]
		UINumber multiplier;
		[SerializeField]
		float multiplierResetTime;
		float resetTimer;
		float multiplierValue;
		float scoreValue;
		[SerializeField]
		GameObject pauseScreen;
		[SerializeField]
		GameObject titleScreen;
		[SerializeField]
		AudioSource backgroundMusic;
		[SerializeField]
		AudioClip titleClip;
		[SerializeField]
		AudioClip backgroundClip;
		[SerializeField]
		AudioClip startingCall;
	
	
		// Use this for initialization
		void Start ()
		{
			multiplierValue = multiplier.Value;
			scoreValue = score.Value;
			player.AddHitCallback (AddMultiplier);
			player.AddKillCallback (AddScore);
			SetStart ();
		}
		
		// Update is called once per frame
		void Update ()
		{
			healthBar.SetHealth (player.Health / player.Stats.maxHealth);
			List<AbilityInfo> abilityInfos = player.GetAbilityInfo ();
			foreach (AbilityIcon i in abilityIcons) {
				AbilityInfo a = abilityInfos [i.index];
				i.icon.UpdateIcon (a.icon, a.cooldown);
			}
			resetTimer += Time.deltaTime;
			if (resetTimer > multiplierResetTime) {
				multiplier.ResetValue ();
				multiplierValue = multiplier.Value;
			}
			if (Input.GetButtonDown("Cancel")) {
				if(Time.timeScale == 1.0F){
					pauseGame ();
				}else{
					unPauseGame();
				}
			}
		}

		// Player scores a hit callback
		void AddMultiplier(){
			multiplierValue += 0.25f;
			multiplier.SetValue (multiplierValue);
			resetTimer = 0;
		}
		// Player scores a kill callback
		void AddScore(float value){
			value *= multiplierValue;
			scoreValue += value;
			killScore.SetValue(value);
			score.SetValue(scoreValue);
		}
		void pauseGame(){
			Time.timeScale = 0.0F;
			Time.fixedDeltaTime = 0.0f;
			backgroundMusic.Pause();
			pauseScreen.SetActive (true);
		}

		void unPauseGame(){
			Time.timeScale = 1.0F;
			Time.fixedDeltaTime = 0.02f;
			backgroundMusic.Play();
			pauseScreen.SetActive (false);
		}

		public void Quit(){
			Application.Quit ();
		}

		public void Restart(){
			unPauseGame ();
			SceneManager.LoadScene (0);
		}

		void SetStart(){
			Time.timeScale = 0.0f;
			Time.fixedDeltaTime = 0.0f;
			backgroundMusic.clip = titleClip;
			backgroundMusic.volume = 1.0f;
			backgroundMusic.Play ();
			titleScreen.SetActive (true);
		}

		public void GoStart(){
			Time.timeScale = 1.0f;
			Time.fixedDeltaTime = 0.02f;
			backgroundMusic.clip = backgroundClip;
			backgroundMusic.volume = 0.03f;
			backgroundMusic.Play ();
			backgroundMusic.PlayOneShot (startingCall, 1.0f);
			titleScreen.SetActive (false);
		}
	}

	[System.Serializable]
	public class AbilityIcon{
		public int index;
		public UIIcon icon;
	}
}
