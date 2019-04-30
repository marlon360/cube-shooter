using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour {

	public Spawner spawner;

	public EnemyWave[] waves;

	int currentWaveIndex = 0;

	public float timeBetweenWaves;

	private IEnumerator spawning;

	int currentNumberOfEnemies = 0;
	int enemiesAlive = 0;

	public UI userInterface;

	public GameEvent WaveStartedEvent;
	public GameEvent WaveCompleteEvent;

	// Update is called once per frame
	void Update () {

	}

	public void StartGame () {

		userInterface.showGame ();
		StartCurrentWave();
		

	}

	public void StartCurrentWave() {
		EnemyWave currentWave;
		if (currentWaveIndex >= waves.Length) {
			currentWave = waves[waves.Length - 1];
		} else {
			currentWave = waves[currentWaveIndex];
		}
		StartCoroutine (StartWave (currentWave));
	}

	IEnumerator StartWave (EnemyWave enemyWave) {

		enemiesAlive = enemyWave.NumberOfEnemies;
		userInterface.showMessageForSeconds ("WAVE " + (currentWaveIndex + 1) + " STARTS", timeBetweenWaves);
		yield return new WaitForSeconds (timeBetweenWaves + 1f);

		WaveStartedEvent.Raise ();

		int spawnedEnemies = 0;

		while (spawnedEnemies < enemyWave.NumberOfEnemies) {

			int parallelSpawns;
			if (enemyWave.NumberOfEnemies - spawnedEnemies >= enemyWave.ParallelSpawns) {
				parallelSpawns = enemyWave.ParallelSpawns;
			} else {
				parallelSpawns = enemyWave.NumberOfEnemies - spawnedEnemies;
			}
			spawning = spawner.SpawnEnemies (enemyWave.EnemyTypes, parallelSpawns);
			StartCoroutine (spawning);
			// add enemies to counter
			spawnedEnemies += parallelSpawns;
			// wait for the next spawn
			yield return new WaitForSeconds (enemyWave.TimeBetweenSpawns);
		}
		currentWaveIndex++;
	}

	public void OnEnemyKilled () {
		enemiesAlive--;
		if(enemiesAlive <= 0) {
			WaveCompleteEvent?.Raise();
			StartCurrentWave();
		}
	}

	public void StopSpawning () {
		StopCoroutine (spawning);
	}

}