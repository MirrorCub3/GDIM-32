using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Joyce Mai
public class Eater : NPC
{   
    [Header("AI Variables")]
    [SerializeField] protected EaterData myData;
    public float chewTime { get; protected set; } // used to tell how long to eat for based on the amount of food eaten
    [SerializeField] private int emptyTolerance = 2; // used to prolong the wander state if the amount of unsuccessful encounters is this
    protected Transform lastTarget;
    protected int emptyCount = 0;

    [Header("Visuals")]
    [SerializeField] protected GameObject hungerBar; // reference to the bar object
    [SerializeField] protected Slider hungerSlider; // reference to the bar used to show how full the NPC is
    [SerializeField] protected GameObject timerObject; // reference to timer object
    [SerializeField] private Slider timerSlider; // referecne to timer bar
    [SerializeField] protected SpriteRenderer sr;

    private void Start()
    {
        agent.speed = myData.Speed();

        hungerSlider.maxValue = myData.HungerLevel();
        hungerSlider.value = 0;
        timerSlider.maxValue = myData.SleepTime();
        timerSlider.value = 0;

        timerObject.SetActive(false);

        lastTarget = null;
        emptyCount = 0;

        if (currState == States.Wander || currState == States.Sleep || currState == States.Target)
        {
            anim.SetTrigger(currState.ToString());
        }
        else
        {
            Debug.Log("The chosen state is an invalid initial state for Eater");
            currState = States.Wander;
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

        if (currState == States.Wander || currState == States.Sleep)
        {
            hungerBar.SetActive(false);
            timerObject.SetActive(currState == States.Sleep);
        }
    }

    public override Data GetData()
    {
        return myData;
    }

    public override void Target()
    {
        if (emptyCount >= emptyTolerance && targets.Count != 1) // used to force the target of a different location if previous was constantly empty
        {
            while (!target || target == lastTarget)
            {
                PickTarget();
            }
            ActiveOnValidTarget();
        }
        else if (emptyCount >= emptyTolerance && targets.Count == 1) // if only one location is avaliable, sleep to delay
        {
            anim.SetTrigger("Sleep");
        }
        else // picking target as usual
        {
            PickTarget();
            ActiveOnValidTarget();
        }
    }

    private void ActiveOnValidTarget()
    {
        sc.enabled = true;
        hungerBar.SetActive(true);
        bubble.SetActive(true);
    }

    public override void AtRestaurant(Restaurant restaurant)
    {
        if (restaurant == null)
        {
            Debug.Log("Cant eat here");
            return;
        }

        sc.enabled = false;
        bubble.SetActive(false);
        Eat(restaurant);
    }
    public virtual void Eat(Restaurant restaurant)
    {
        int amountBought = restaurant.BuyProduct(Mathf.Min(myData.FeedRate(), (int)(hungerSlider.maxValue - hungerSlider.value))); // buys less than the npc's feed rate if there's not enough room
        hungerSlider.value += amountBought;
        chewTime = myData.ChewSpeed() * amountBought;

        if (amountBought <= 0) // if unsuccessful purchase
        {
            if (target == lastTarget)
            {
                print("eating nothing");
                emptyCount++;
            }
            else
            {
                lastTarget = target;
                emptyCount = 1;
            }
            anim.SetTrigger("Wander");

        }
        else
            anim.SetTrigger("Eat");
    }
    public bool IsFull()
    {
        return hungerSlider.value >= hungerSlider.maxValue;
    }

    public void Sleep()
    {
        target = null;
        agent.isStopped = true;

        timerObject.SetActive(true);
        timerSlider.value = 0;

        sr.sprite = myData.SleepSprite();
        sc.enabled = false;
        hungerSlider.value = 0;
    }
    public void WakeUp()
    {
        sr.sprite = myData.NormalSprite();
        timerObject.SetActive(false);
        timerSlider.value = 0;
        agent.isStopped = false;
        sc.enabled = true;
        emptyCount = 0;
    }
    public void SetTimePassed(float time)
    {
        timerSlider.value = Mathf.Min(timerSlider.maxValue, time);
    }

}

