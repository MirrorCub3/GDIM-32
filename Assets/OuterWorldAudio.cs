using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterWorldAudio : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        // audio control initialize
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
