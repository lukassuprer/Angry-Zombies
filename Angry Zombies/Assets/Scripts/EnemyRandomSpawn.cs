using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomSpawn : MonoBehaviour
{
    public GameObject[] SpawnPoints;
    public int zombieNumber;
    public GameObject zombiePrefab;
    private float lastSpawn;
    public float spawnRate;
    public int randomSpawnPoint = 0;
    ObjectPooler objectPooler;

    private void Start() {
        objectPooler = ObjectPooler.Instance;
    }

    void Update()
    {
        //First checks if it can spawn, after that spawns given number of zombies at random positions that are set by in game spawnpoints
        if (Time.time > spawnRate + lastSpawn && spawnRate > 0)
        {
            if(spawnRate <= 0){
                spawnRate = 1;
            }
            if(zombieNumber > 10){
                zombieNumber = 10;
            }
            int number = Random.Range(1, zombieNumber);
            for (int i = 0; i < zombieNumber; i++)
            {
                randomSpawnPoint = Random.Range(0, SpawnPoints.Length);
                objectPooler.SpawnFromPool("Zombie", SpawnPoints[randomSpawnPoint].transform.position, Quaternion.identity);
            }
            lastSpawn = Time.time;
            zombieNumber += 1;
            spawnRate -= 0.3f;
        }
    }
}
