using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
[CreateAssetMenu(menuName = "Enemy/EaterData")]
public class EaterData : ScriptableObject
{
    [Header("Hunger State")]
    public float hungerLevel = 100; // the amount it takes before full
    public int feedRate = 0; // the amount of food they take at a time

    [Header("Idle State")]
    public float idleTime = 10; // the amount of time they wait before targeting again

    [Header("Sleep State")]
    public float sleepTime; 
}
