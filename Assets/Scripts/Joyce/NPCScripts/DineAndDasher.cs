using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Joyce Mai
public class DineAndDasher : Visitor
{
    [Header("Dine N' Dash")]
    [SerializeField] private int theftAmount;
    [SerializeField] private Transform hideSpot;

    private void Start()
    {
        agent.speed = myData.Speed();
        GetLocations();

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
        }
    }
}
