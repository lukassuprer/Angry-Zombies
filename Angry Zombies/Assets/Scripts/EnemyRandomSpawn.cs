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
                GameObject GO = Instantiate(zombiePrefab, SpawnPoints[x].transform.position, Quaternion.identity);
                // int offset = Random.Range(1, 10);
                // GO.transform.position = new Vector3(GO.transform.position.x + offset, GO.transform.position.y, GO.transform.position.z + offset);
                x++;
                if (x > 3)
                {
                    x = 0;
                }
            }
            lastSpawn = Time.time;
            zombieNumber += 1;
            spawnRate -= 0.3f;
        }
    }
}
