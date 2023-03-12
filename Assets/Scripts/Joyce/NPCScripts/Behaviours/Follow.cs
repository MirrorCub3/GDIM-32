using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class Follow : Strategy
{
    private float margin = .3f;
    void OnEnable()
    {
        agent.speed = speed;
    }

    void Update()
    {
        if (Mathf.Abs(Vector3.Distance(player.position, transform.position)) > agent.stoppingDistance + margin)
        {
            agent.SetDestination(player.position);
            anim.SetFloat("Speed", speed);
            Flip();
        }
        else
            anim.SetFloat("Speed", 0);
    }
}
