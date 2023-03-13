using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class Follow : Strategy
{
    [SerializeField] private float margin = .3f; // used to add buffer between stopping distance, helps animation transitions
    void OnEnable()
    {
        agent.speed = speed;
    }

    void Update()
    {
        if (Mathf.Abs(Vector3.Distance(player.position, transform.position)) > agent.stoppingDistance + margin) // if the player is far, go to them, set animation to move
        {
            agent.SetDestination(player.position);
            anim.SetFloat("Speed", speed);
            Flip();
        }
        else
            anim.SetFloat("Speed", 0);
    }
}
