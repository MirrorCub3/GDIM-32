using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class Collect : Strategy
{
    private Transform target; // reference to current target
    private bool targetSet; // boolean to track when to dequeue
    void OnEnable()
    {
        agent.speed = speed;
        target = null;
        targetSet = false;
        TargetCard();
    }

    void Update()
    {
        if ( target != null)
        {
            agent.SetDestination(target.position);
            anim.SetFloat("Speed", speed);
            Flip();
        }
        else
        {
            TargetCard();
            anim.SetFloat("Speed", 0);
        }
    }

    private void TargetCard() // sets a new target or removes all null entries till empty
    {
        if (targetSet) // if the target had been set prior and is now null, remove it from the queue
            me.cardLocs.Dequeue();
        while (target == null && me.cardLocs.Count > 0) // keep picking spots until valid or out of spots
        {
            target = me.cardLocs.Peek(); // set the target to the first location in queue without removing
            if (target == null) // if the spot is null, remove it
                me.cardLocs.Dequeue();
        }

        targetSet = me.cardLocs.Count > 0;
    }
}
