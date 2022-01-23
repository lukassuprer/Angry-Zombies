using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static PlayerController playerInstance = null;
    public static bool gameStart;
    public GameObject player;
    public GameObject enemySpawner;

    private void Update()
    {
        if (gameStart == true)
        {
            player.SetActive(true); 
            enemySpawner.SetActive(true);
        }
        else{
            
        }
    }

}
