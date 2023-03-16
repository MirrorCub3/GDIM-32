using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Jessica Lam
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // reference to this singleton
    
    // book keeping variables for scene management
    private string currScene;
    private string prevScene;

    [SerializeField] Sound[] sounds;

    void Awake()
    {
        // singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.setSource(gameObject);
            s.setClip();
            s.setVolume();
            s.setPitch();
        }
    }

    void Update()
    {
        currScene = SceneManager.GetActiveScene().name;
        if (prevScene != currScene)
        {
            UpdateMusic();
            prevScene = currScene;
        }
    }

    void UpdateMusic()
    {
        AudioSource[] audios = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in audios)
        {
            audio.Stop();
        }

        if (currScene == "MainMenu")
        {
            Play("Main Menu");
        }
        else if (currScene.Contains("OuterWorld"))
        {
            Play("Outer World");
        }
        else if (currScene == "CremeBruleeKitchen") // in-game testing, remove later
        {
            Play("CremeBruleeKitchen");
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.getName() == name);
        s.playSource();
    }
}
