using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        //List of sound settings
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        [HideInInspector]
        public float volume;
        [Range(0.1f, 3f)]
        public float pitch;
        [HideInInspector]
        public AudioSource source;
        public int priority;
    }
    //Just manages ingame sound. Assigns settings from sound list and plays sounds
    public Sound[] sounds;
    public void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = PlayerPrefs.GetFloat("volume");
            s.source.pitch = s.pitch;
            s.source.priority = s.priority;
        }
    }
    private void Start()
    {
        InvokeRepeating("RandomSound", 5f, 5f);
    }
    private void Update(){
        if(GameManager.unpaused == true){
            SetVolume();
            GameManager.unpaused = false;
        }
    }
    public void SetVolume(){
        foreach (Sound s in sounds)
        {
            if (PlayerPrefs.HasKey("volume"))
            {
                s.source.volume = PlayerPrefs.GetFloat("volume");
            }
            else
            {
                s.source.volume = 0.1f;
            }
        }
    }
    private void RandomSound()
    {
        int soundNumber = UnityEngine.Random.Range(2, 6);
        switch (soundNumber)
        {
            case 2:
                Play("zombie burp");
                break;
            case 3:
                Play("zombie moan");
                break;
            case 4:
                Play("zombie moan1");
                break;
            case 5:
                Play("zombie moan2");
                break;
            default:
                Play("zombie moan");
                break;
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("sound name is wrong");
            return;
        }
        s.source.PlayOneShot(s.source.clip);
    }
}
