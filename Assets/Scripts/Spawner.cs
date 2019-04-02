using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public Transform[] SpawnPoints;

    public GameObject SpawnIntro;
    public float SpawnIntroTime = 1;


    public IEnumerator SpawnEnemies(Enemy[] enemyTypes, int parallelSpawns) {

        // new array for the next spawn points
        Transform[] nextSpawnPoints = new Transform[parallelSpawns];

        // a list of all spawn points, which are not one of the next spawn points
        // copy from all spawn points
        List<Transform> freeSpawnPoints = new List<Transform>(SpawnPoints);

        for (int i = 0; i < parallelSpawns; i++)
        {
            // choose a random index of the list of spawn points, which are not one of the next spawn points
            int randomIndex = Random.Range(0, freeSpawnPoints.Count);
            // get the actual spawn point with the random index
            Transform randomSpawnPoint = freeSpawnPoints[randomIndex];
            // add the chosen spawnpoint to the list of next spawn points
            nextSpawnPoints[i] = randomSpawnPoint;
            // remove the spawn point from the free spawn point list
            freeSpawnPoints.RemoveAt(randomIndex);

            // Instantiate the Spawn Intro and Destroy it after time
            Destroy (Instantiate (SpawnIntro, randomSpawnPoint.position, randomSpawnPoint.rotation) as GameObject, 3f);
        }

        // wait for the intro to end
        yield return new WaitForSeconds (SpawnIntroTime);
        
        
        for (int i = 0; i <parallelSpawns; i++)
        {
            // choose a random index of the enemy types
            int randomIndex = Random.Range(0, enemyTypes.Length);
            // get the actual enemy
            Enemy enemy = enemyTypes[randomIndex];

            // Instantiate the Enemy at the spawn point
            Instantiate (enemy, nextSpawnPoints[i].position, nextSpawnPoints[i].rotation);
        }
    } 

}
