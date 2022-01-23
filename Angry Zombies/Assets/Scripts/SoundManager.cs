using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
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
    void Update()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = PlayerPrefs.GetFloat("volume");
        }
    }
    private void RandomSound(){
        int soundNumber = UnityEngine.Random.Range(2, 6);
        switch (soundNumber)
        {
            case 2:
                FindObjectOfType<SoundManager>().Play("zombie burp");
                break;
            case 3:
                FindObjectOfType<SoundManager>().Play("zombie moan");
                break;
            case 4:
                FindObjectOfType<SoundManager>().Play("zombie moan1");
                break;
            case 5:
                FindObjectOfType<SoundManager>().Play("zombie moan2");
                break;
        }
        Play("zombie moan");
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
