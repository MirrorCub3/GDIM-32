using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Jessica Lam
[System.Serializable]
public class Sound {
    // AudioManager script accesses these
    [SerializeField] string name;
    [SerializeField] AudioClip clip;

    [Range(0f,1f)]
    [SerializeField] float volume;
    [Range(0.1f, 3f)]
    [SerializeField] float pitch;

    [HideInInspector]
    [SerializeField] AudioSource source;

    public string getName()
    {
        return name;
    }

    public void setSource(GameObject audioManager)
    {
        source = audioManager.AddComponent<AudioSource>();
    }

    public void setClip()
    {
        source.clip = clip;
    }

    public void setVolume()
    {
        source.volume = volume;
    }

    public void setPitch()
    {
        source.pitch = pitch;
    }

    public void playSource()
    {
        source.Play();
    }
}
