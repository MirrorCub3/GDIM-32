using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Eat : StateMachineBehaviour
{
    [Header("Data From NPC")]
    private Eater script;
    private NavMeshAgent agent;

    [Header("Bookkeeping")]
    private float time;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;

        script = animator.gameObject.GetComponent<Eater>();
        script.SetCurrState(NPC.States.Eat);
        agent = script.GetAgent();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (script.paused) return;

        time += Time.deltaTime;
        if (time >= script.chewTime) // determines how to proceed out of eat state
        {
            if(script.IsFull())
                animator.SetTrigger("Sleep");
            else
                animator.SetTrigger("Wander");
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                script.Wander();
            }
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Eat");
    }
}
