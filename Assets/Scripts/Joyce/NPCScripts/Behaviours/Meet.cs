using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class Meet : Strategy
{
    void OnEnable()
    {
        agent.speed = speed;
    }

    void Update()
    {
        if (Mathf.Abs(Vector3.Distance(player.position, transform.position)) > agent.stoppingDistance) // if the player is far, go to them, set animation to move
        {
            agent.SetDestination(player.position);
            anim.SetFloat("Speed", speed);
            Flip();
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }
}
