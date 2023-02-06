using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai & Naman Khurana
public class Eater : NPC
{
    [Header("AI Variables")]
    [SerializeField] private EaterData myData;

    void Update()
    {
        // Naman Khurana
        if (target != null)
        {
            agent.SetDestination(target.position);
            agent.updateRotation = false; // this keeps the sprite facing the camera
        }
    }

    public override void AtRestaurant()
    {
        Debug.Log("nom nom");
    }

}

