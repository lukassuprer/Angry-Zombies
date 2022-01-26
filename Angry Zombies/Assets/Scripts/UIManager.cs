using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider mainSlider;
    public Slider mainSlider1;
    public Text health;
    public PlayerController playerController;
    public WeaponScript weaponScript;
    public float sliderValue;
    public GameObject pauseMenu;
    public GameObject hud;
    public Text weaponName;
    private void Start() {
        if(PlayerPrefs.HasKey("volume")){
            mainSlider.value = PlayerPrefs.GetFloat("volume");
        }
        else{
            mainSlider.value = 0.1f;
        }
    }   
    public void SubmitSliderSetting()
    {
        if(GameManager.isPlaying == false){
            PlayerPrefs.SetFloat("volume", mainSlider1.value);
            PlayerPrefs.Save();
        }
        else{
            PlayerPrefs.SetFloat("volume", mainSlider.value);
            PlayerPrefs.Save();
        }
    }
    private void Update(){
        SubmitSliderSetting();
        if(Input.GetKeyDown(KeyCode.Escape)){
            Pause();
        }
        if(hud.activeSelf == true){
            Health();
        }
        WeaponName();
    }
    public void Health(){
        health.text = playerController.health.ToString();
        if(playerController.health <= 0){
            health.color = Color.red;
        }
    }
    public void WeaponName(){
        weaponName.text = weaponScript.currentGun.weaponName;
        Debug.Log(weaponScript.currentGun.weaponName);
    }
    public void Play(){
        GameManager.gameStart = true;
    }
    public void Exit(){
        Application.Quit();
        Debug.Log("EXIT hahah");
    }
    public void Pause(){
        if(pauseMenu.activeSelf == false){
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else{
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
