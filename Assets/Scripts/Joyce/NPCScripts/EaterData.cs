using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
[CreateAssetMenu(menuName = "Enemy/EaterData")]
public class EaterData : ScriptableObject
{
    [Header("Hunger State")]
    [SerializeField] private float hungerLevel = 100; // the amount it takes before full
    [SerializeField] private int feedRate = 0; // the amount of food they take at a time
    [SerializeField] private float speed = 3;

    [Header("Idle State")]
    [SerializeField] private float idleTime = 10; // the amount of time they wait before targeting again

    [Header("Sleep State")]
    [SerializeField] private float sleepTime; 

    public float  HungerLevel() // gets the hunger level
    {
        return hungerLevel;
    }

    public int FeedRate() // gets the feed rate
    {
        return feedRate;
    }

    public float IdleTime() // gets the idle time
    {
        return idleTime;
    }

    public float SleepTime() // gets the sleep time
    {
        return sleepTime;
    }

    public float Speed()
    {
        return speed;
    }

}
