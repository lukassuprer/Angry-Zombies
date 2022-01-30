using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static PlayerController playerInstance = null;
    public static bool gameStart;
    public static bool gameExit;
    public static bool isPlaying;
    public GameObject player;
    public GameObject enemySpawner;
    public GameObject soundManager;
    public GameObject pauseButton;
    public GameObject hud;
    public GameObject inventory;
    public Slider mainSlider;
    public Slider mainSlider1;
    public ZombieScript zombieScript;
    public static bool addedScore;
    public static int score;

    private void Update()
    {
        //Game start activated all needed components
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
            addedScore = false;
            Time.timeScale = 1;
            score = 0;
        }
        else{
        }
        //On game exit deactivate these components
        if(gameExit == true){
            player.SetActive(false); 
            enemySpawner.SetActive(false);
            soundManager.SetActive(false);
            pauseButton.SetActive(false);
            hud.SetActive(false);

            mainSlider1.value = PlayerPrefs.GetFloat("volume");
            gameStart = false;
            isPlaying = false;
            gameExit = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
