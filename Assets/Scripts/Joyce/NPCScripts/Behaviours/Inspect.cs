using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inspect : StateMachineBehaviour
{
    [Header("Data From NPC")]
    private VisitorData data;
    private Visitor script;

    [Header("Bookkeeping")]
    private float waitTime;
    private float time;
    private bool exited;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;
        exited = false;

        script = animator.gameObject.GetComponent<Visitor>();
        data = script.GetData() as VisitorData;
        script.SetCurrState(NPC.States.Inspect);

        waitTime = data.VistTime();
        script.Visit(waitTime);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (script.paused) return;

        if (exited) return;

        time += Time.deltaTime;
        if (time >= waitTime)
        {
            script.DoneVisiting();
            exited = true;
        }
        script.SetTimePassed(time);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Inspect");
    }
}
