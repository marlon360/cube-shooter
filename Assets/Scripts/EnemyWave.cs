using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyWave : ScriptableObject {

    public Enemy[] EnemyTypes;

    public int NumberOfEnemies = 1;

    public int ParallelSpawns = 1;

    public int TimeBetweenSpawns = 3;

}