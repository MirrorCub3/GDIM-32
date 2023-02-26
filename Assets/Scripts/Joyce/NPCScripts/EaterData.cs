using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
[CreateAssetMenu(menuName = "Enemy/EaterData")]
public class EaterData : Data
{
    [Header("Hunger State")]
    [SerializeField] private float hungerLevel = 100; // the amount it takes before full
    [SerializeField] private int feedRate = 1; // the amount of food they take at a time

    [Header("Eat State")]
    [SerializeField] private float chewSpeed = 2f; // amount of time to eat each food

    [Header("Sleep State")]
    [SerializeField] private float sleepTime;

    [Header("Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite sleepSprite;

    public void IncreaseHunger() // can be used later in the game to increase difficulty
    {
        hungerLevel++;
    }
    public float  HungerLevel() // gets the hunger level
    {
        return hungerLevel;
    }

    public int FeedRate() // gets the feed rate
    {
        return feedRate;
    }
    public float SleepTime() // gets the sleep time
    {
        return sleepTime;
    }

    public float ChewSpeed()
    {
        return chewSpeed;
    }
    public Sprite NormalSprite()
    {
        return normalSprite;
    }
    public Sprite SleepSprite()
    {
        return sleepSprite;
    }
}
