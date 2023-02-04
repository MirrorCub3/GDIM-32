using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
[CreateAssetMenu(menuName = "Enemy/EaterData")]
public class EaterData : ScriptableObject
{
    public float hungerLevel { get; private set;}
    public float idleTime { get; private set;}
}
