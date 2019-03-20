using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour {


	public Transform[] spawnPoints;
	public Enemy[] enemies;

	int currentWave = 1;

	public float timeBetweenWaves;
	public float timeBetweenSpawns = 1f;
	public GameObject spawnIntro; //Was soll vor dem Spawn passieren?

	private IEnumerator spawning;

	int currentNumberOfEnemies = 0;
	int enemiesAlive = 0;

	public UI userInterface;

	public static event System.Action OnWaveStart;
	
	// Update is called once per frame
	void Update () {
		
	}
		
		
	IEnumerator startSpawn() {

		userInterface.showGame ();

		while(true){


			int numberOfEnemies = currentWave*3;
			userInterface.showMessageForSeconds ("WAVE "+currentWave+" STARTS",timeBetweenWaves);
			yield return new WaitForSeconds (timeBetweenWaves+1f);
			enemiesAlive = numberOfEnemies*2;

			if (OnWaveStart != null) {
				OnWaveStart ();
			}

			while (currentNumberOfEnemies < numberOfEnemies) {
				Transform spawnPunkt = spawnPoints [Random.Range (0, spawnPoints.Length)];
				Transform spawnPunkt2 = spawnPoints [Random.Range (0, spawnPoints.Length)];

				while (spawnPunkt.position == spawnPunkt2.position) {
					spawnPunkt2 = spawnPoints [Random.Range (0, spawnPoints.Length)];
				}

				Destroy (Instantiate (spawnIntro, spawnPunkt.position, spawnPunkt.rotation) as GameObject, 3f);
				Destroy (Instantiate (spawnIntro, spawnPunkt2.position, spawnPunkt2.rotation) as GameObject, 3f);
				yield return new WaitForSeconds (1);

				Instantiate (enemies [Random.Range (0, enemies.Length)], spawnPunkt.position, spawnIntro.transform.rotation);
				Instantiate (enemies [Random.Range (0, enemies.Length)], spawnPunkt2.position, spawnIntro.transform.rotation);
				currentNumberOfEnemies++;
				yield return new WaitForSeconds (timeBetweenSpawns);
			}
			yield return new WaitWhile (() => enemiesAlive != 0);
			currentNumberOfEnemies = 0;
			currentWave++;
		}


	}
	public void OnEnemyKilled(Transform transform){
		enemiesAlive--;

	}

	public void startSpawning(){
		spawning = startSpawn ();
		StartCoroutine (spawning);
	}

	public void StopSpawning(){
		StopCoroutine(spawning);
	}

}
	


