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
    private int x = 0;
    ObjectPooler objectPooler;

    private void Start() {
        objectPooler = ObjectPooler.Instance;
    }

    void Update()
    {
        if (Time.time > spawnRate + lastSpawn && spawnRate > 0)
        {
            if(spawnRate <= 0){
                spawnRate = 1;
            }
            if(zombieNumber > 10){
                zombieNumber = 10;
            }
            //int point = Random.Range(1, SpawnPoints.Length);
            int number = Random.Range(1, zombieNumber);
            for (int i = 0; i < zombieNumber; i++)
            {
                x = Random.Range(0, SpawnPoints.Length);
                objectPooler.SpawnFromPool("Zombie", SpawnPoints[x].transform.position, Quaternion.identity);
            }
            lastSpawn = Time.time;
            zombieNumber += 1;
            spawnRate -= 0.3f;
        }
    }
}
