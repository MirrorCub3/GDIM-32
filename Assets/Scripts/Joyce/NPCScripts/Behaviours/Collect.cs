using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class Collect : Strategy
{
    private Transform target;
    private bool targetSet;
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

    private void TargetCard()
    {
        if (targetSet) // if the target had been set prior and is now null, remove it from the queue
            me.cardLocs.Dequeue();
        while (target == null && me.cardLocs.Count > 0)
        {
            target = me.cardLocs.Peek(); // set the target to the first location in queue without removing
            if (target == null)
                me.cardLocs.Dequeue();
        }

        targetSet = me.cardLocs.Count > 0;
    }
}
