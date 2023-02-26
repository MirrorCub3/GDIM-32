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
    public int VistTime()
    {
        return Random.Range(visitTimeMin, visitTimeMax);
    }
}
