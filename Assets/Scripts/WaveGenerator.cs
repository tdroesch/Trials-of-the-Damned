using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using OmegaFramework;

public class WaveGenerator : MonoBehaviour {
	[System.Serializable]
	public class Wave{
		public List<GameObject> spawnPoints;
		public List<GameObject> Enemy;
	}
	[SerializeField]
	UnitManager player;
	[SerializeField]
	List<Wave> Waves;
	[SerializeField]
	Wave randomSpawns;
	int currentWave;

	int enemyCount;
	float Wavetimer;
	[SerializeField]
	float WaveTimelimit;
	[SerializeField]
	AudioClip waveStart;
	[SerializeField]
	AudioClip waveClear;

	AudioSource soundSource;

	// Use this for initialization
	void Start () {
		Wavetimer = 5;
		soundSource = GetComponent<AudioSource>();
		player.AddKillCallback (EnemyKilled);
	}
	
	// Update is called once per frame
	void Update () {
		Wavetimer -= Time.deltaTime;

		if (enemyCount <= 0) {
			if(currentWave< Waves.Count && Wavetimer>5)
			{
				Wavetimer=5;
				soundSource.PlayOneShot(waveClear, 1.0f);
			}
		}
		if(Wavetimer<=0){
			if (currentWave < Waves.Count) {
				SpawnEnemies ();
				soundSource.PlayOneShot (waveStart, 0.5f);
				currentWave++;
//				if (currentWave >= Waves.Count) currentWave = Waves.Count-1;
				Wavetimer = WaveTimelimit;
			} else {
				Instantiate (randomSpawns.Enemy [Random.Range (0, randomSpawns.Enemy.Count)],
					randomSpawns.spawnPoints [Random.Range (0, randomSpawns.spawnPoints.Count)].transform.position,
					Quaternion.identity);
				WaveTimelimit = WaveTimelimit > 5 ? 5 : WaveTimelimit * 0.97f;
				Wavetimer = WaveTimelimit;
			}
		}
	}

	void SpawnEnemies(){
		for (int i=0; i<Waves[currentWave].Enemy.Count; i++)
		{
			Instantiate(Waves[currentWave].Enemy[i],Waves[currentWave].spawnPoints[i].transform.position, Quaternion.identity);
			enemyCount++;
		}

	}

	void EnemyKilled(float score){
		enemyCount--;
	}
}
