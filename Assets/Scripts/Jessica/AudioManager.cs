using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

// Jessica Lam
public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sounds;

    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.setSource(gameObject);
            s.setClip();
            s.setVolume();
            s.setPitch();
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.getName() == name);
        s.playSource();
    }
}
