using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static PlayerController playerInstance = null;
    public static bool gameStart;
    public static bool isPlaying;
    public GameObject player;
    public GameObject enemySpawner;
    public GameObject soundManager;
    public GameObject pauseButton;
    public GameObject hud;
    public Slider mainSlider;

    private void Update()
    {
        if (gameStart == true)
        {
            player.SetActive(true); 
            enemySpawner.SetActive(true);
            soundManager.SetActive(true);
            pauseButton.SetActive(true);
            hud.SetActive(true);

            mainSlider.value = PlayerPrefs.GetFloat("volume");
            gameStart = false;
            isPlaying = true;
        }
        else{
        }
    }

}
