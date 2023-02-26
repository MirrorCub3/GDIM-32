using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
[CreateAssetMenu(menuName = "Enemy/VisitorData")]
public class VisitorData : Data
{
    [Header("Visit State")]
    [SerializeField] private int visitTimeMin;
    [Tooltip("vistTimeMax is non inclusive")]
    [SerializeField] private int visitTimeMax;

    public int VisitTimeMin() // gets the visit time
    {
        return visitTimeMin;
    }
    
    public int VisitTimeMax()
    {
        return visitTimeMax;
    }
}
