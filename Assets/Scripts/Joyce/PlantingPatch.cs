using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class PlantingPatch : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            print("triggered!!");
        }
    }
}
