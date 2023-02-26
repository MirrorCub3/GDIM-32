using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : StateMachineBehaviour
{
    [Header("Data From NPC")]
    private NPC script;
    private NavMeshAgent agent;

    [Header("State Timeout")]
    [SerializeField] private float timeout = 3f;
    private float time;
    private bool reached;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;
        reached = false;

        script = animator.gameObject.GetComponent<NPC>();
        script.Target();
        script.SetCurrState(NPC.States.Target);
        agent = script.GetAgent();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (script.paused || !agent.enabled) return;

        if (reached)
        {
            time += Time.deltaTime;

            if (time >= timeout)
            {
                Debug.Log("timeout wander");
                animator.SetTrigger("Wander");
                return;
            }

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                script.Wander();
            }
        }
        else if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            reached = true;
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("exited target");
        animator.ResetTrigger("Target");
    }
}
