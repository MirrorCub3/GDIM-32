using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Joyce Mai
public class Wander : StateMachineBehaviour
{
    [Header("Data From NPC")]
    private Data data;
    private NPC script;
    private NavMeshAgent agent;

    [Header("Bookkeeping")]
    private float time;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;

        script = animator.gameObject.GetComponent<NPC>();
        data = script.GetData();
        script.SetCurrState(NPC.States.Wander);
        agent = script.GetAgent();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (script.paused) return;

        time += Time.deltaTime;
        if (time >= data.IdleTime())
        {
            animator.SetTrigger("Target");
        }
        else
        {
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                script.Wander();
            }
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Wander");
    }
}
