using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : StateMachineBehaviour
{
    [Header("Data From NPC")]
    private VisitorData data;
    private DineAndDasher script;

    [Header("Bookkeeping")]
    private float time;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;

        script = animator.gameObject.GetComponent<DineAndDasher>();
        data = script.GetData() as VisitorData;
        script.SetCurrState(NPC.States.Wander);
        script.Visit(data.IdleTime());
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (script.paused) return;

        time += Time.deltaTime;
        if (time >= data.IdleTime())
        {
            animator.SetTrigger("Target");
            return;
        }
        script.SetTimePassed(time);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Wander");
    }
}
