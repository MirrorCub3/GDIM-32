using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
[CreateAssetMenu(menuName = "Enemy/VisitorData")]
public class VisitorData : ScriptableObject
{
    [Header("Visit State")]
    [SerializeField] private int visitTimeMin;
    [Tooltip("vistTimeMax is non inclusive")]
    [SerializeField] private int visitTimeMax;
    [SerializeField] private float speed = 3;

    [Header("Idle State")]
    [SerializeField] private int idleTime;

    public int VisitTimeMin() // gets the visit time
    {
        return visitTimeMin;
    }
    
    public int VisitTimeMax()
    {
        return visitTimeMax;
    }

    public float Speed()
    {
        return speed;
    }

    public int IdleTime()
    {
        return idleTime;
    }
}
