using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource, walkSource;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // PlayMusic("Theme");
        PlayMusic("Background");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null) {
            Debug.Log("Sound not found");
        }

        else {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void playSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null) {
            Debug.Log("Sound not found");
        }

        else {

            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void playWalk(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        
        if (s == null) {
            Debug.Log("Sound not found");
        }

        else {

            walkSource.PlayOneShot(s.clip);
        }
        
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}

