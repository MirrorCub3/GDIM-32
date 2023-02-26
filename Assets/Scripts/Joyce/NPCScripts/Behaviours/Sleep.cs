using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : StateMachineBehaviour
{
    [Header("Data From NPC")]
    private EaterData data;
    private Eater script;

    [Header("Bookkeeping")]
    private float time;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;

        script = animator.gameObject.GetComponent<Eater>();
        data = script.GetData();
        script.SetCurrState(NPC.States.Sleep);
        script.Sleep();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;
        if (time >= data.SleepTime())
        {
            animator.SetTrigger("Wander");
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Sleep");
        script.WakeUp();
    }
}
