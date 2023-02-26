using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DineAndDasher : Visitor
{
    [Header("Dine N' Dash")]
    [SerializeField] private int theftAmount;
    [SerializeField] private Transform hideSpot;

    private void Awake()
    {
        // Naman Khurana
        agent = GetComponent<NavMeshAgent>();

        // Joyce Mai
        sc = GetComponent<SphereCollider>();
        agent.updateRotation = false; // this keeps the sprite facing the camera
        agent.speed = myData.Speed();

        timerObject.SetActive(false);

        if (currState == States.Target)
        {
            anim.SetTrigger(currState.ToString());
        }
        else
        {
            currState = States.Wander;
            GoHide();
        }
    }

    public override void Target()
    {
        base.Target();
        agent.enabled = false;
        timerObject.SetActive(false);
    }

    public override void AtRestaurant(Restaurant restaurant)
    {
        base.AtRestaurant(restaurant);
        if (restaurant != null)
            Steal(restaurant);
    }

    private void Steal(Restaurant restaurant)
    {
        restaurant.StealProduct(theftAmount);
    }
    private void GoHide()
    {
        target = hideSpot;
        timerObject.SetActive(false);
        sc.enabled = true;
    }
    public override void DoneVisiting()
    {
        print("done here");
        timerObject.SetActive(false);
        GoHide();
    }

    public override void AdditionalTrigger(Collider other)
    {
        if (other.transform == hideSpot && hideSpot == target)
        {
            print("at my hidng spot");
            timerObject.SetActive(true);
            anim.SetTrigger("Wander");
            agent.enabled = false;
        }
    }
}
