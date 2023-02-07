using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Joyce Mai & Naman Khurana
public class Eater : NPC
{
    [Header("AI Variables")]
    [SerializeField] private EaterData myData;
    [SerializeField] private int idleThreshold = 0; // the amount of targets minimum in the list before idle state is triggered
    [SerializeField] private Transform idleSpot; // temporary unit wander behavior is implemented

    [Header("UI")]
    [SerializeField] private GameObject hungerBar; // reference to the bar object
    [SerializeField] private Slider hungerSlider; // reference to the bar used to show how full the NPC is

    private void Awake()
    {
        // Naman Khurana
        agent = GetComponent<NavMeshAgent>();

        // Joyce Mai
        sc = GetComponent<SphereCollider>();
        agent.updateRotation = false; // this keeps the sprite facing the camera
        agent.speed = myData.Speed();

        hungerSlider.maxValue = myData.HungerLevel();
        hungerSlider.value = 0;

        PickTarget();
    }

    void Update()
    {
        // Naman Khurana
        if (target != null)
        {
            agent.SetDestination(target.position);
            agent.updateRotation = false; // this keeps the sprite facing the camera
        }
    }

    public override void AtRestaurant(Restaurant restaurant)
    {
        if (restaurant == null)
        {
            Debug.Log("Cant eat here");
            return;
        }

        sc.enabled = false;
        Eat(restaurant);
        sc.enabled = true;
    }

    public override void AdditionalTrigger(Collider other)
    {
        if (other.transform == idleSpot && idleSpot == target)
        {
            Debug.Log("i'm chewing");
            if (hungerSlider.value >= hungerSlider.maxValue)
                StartCoroutine(Sleep());
            else
                StartCoroutine(Wandering());
        }
    }

    private void Eat(Restaurant restaurant)
    {
        print("nom nom");
        hungerSlider.value += restaurant.BuyProduct(myData.FeedRate());
        target = idleSpot;
    }

    protected IEnumerator Wandering()
    {
        yield return new WaitForSeconds(myData.IdleTime());
        PickTarget();
    }

    protected IEnumerator Sleep()
    {
        hungerBar.SetActive(false);
        yield return new WaitForSeconds(myData.SleepTime());
        hungerBar.SetActive(true);
        hungerSlider.value = 0;
        PickTarget();
    }

}

