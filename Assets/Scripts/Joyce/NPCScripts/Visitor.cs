using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Joyce Mai and Naman Khurana
public class Visitor : NPC
{
    [Header("AI Variables")]
    [SerializeField] protected VisitorData myData;

    [Header("UI")]
    [SerializeField] protected GameObject timerObject;
    [SerializeField] private Slider timerSlider;
    private void Awake()
    {
        // Naman Khurana
        agent = GetComponent<NavMeshAgent>();

        // Joyce Mai
        sc = GetComponent<SphereCollider>();
        agent.updateRotation = false; // this keeps the sprite facing the camera
        agent.speed = myData.Speed();

        timerObject.SetActive(false);

        if (currState == States.Target || currState == States.Wander)
        {
            anim.SetTrigger(currState.ToString());
        }
        else
        {
            Debug.Log("The chosen state is an invalid initial state for Visitor");
            anim.SetTrigger("Wander");
        }
    }

    void Update()
    {
        // Naman Khurana
        if (target != null && agent.enabled)
        {
            agent.SetDestination(target.position);
        }
    }
    public override Data GetData()
    {
        return myData;
    }

    public void Visit(float time)
    {
        timerSlider.maxValue = time;
        timerObject.SetActive(true);
        target = null;
    }
    public void SetTimePassed(float time)
    {
        timerSlider.value = Mathf.Min(timerSlider.maxValue, time);
    }

    public virtual void DoneVisiting()
    {
        timerObject.SetActive(false);
        anim.SetTrigger("Wander");
    }

    public override void AtRestaurant(Restaurant restaurant) // defines what this AI does when at the restuarant
    {
        sc.enabled = false;
        anim.SetTrigger("Inspect");
    }
}
