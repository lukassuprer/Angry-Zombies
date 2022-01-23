using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider mainSlider;
    public float sliderValue;
    private void Start() {
        //sliderValue = GameObject.Find("Volume Slider").GetComponent<Slider>().value;
        // mySlider = GetComponent<Slider>();
        mainSlider.value = PlayerPrefs.GetFloat("volume");
    }   
    public void SubmitSliderSetting()
    {
        //Displays the value of the slider in the console.
        Debug.Log(mainSlider.value);
        PlayerPrefs.SetFloat("volume", mainSlider.value);
        PlayerPrefs.Save();
        //mainSlider.value = FindObjectOfType<SoundManager>().audio.volume;
    }
    private void Update(){
        //Debug.Log(mySlider.value);
        SubmitSliderSetting();
    }
    public void Play(){
        GameManager.gameStart = true;
    }
    public void Exit(){
        Application.Quit();
        Debug.Log("EXIT hahah");
    }
}
