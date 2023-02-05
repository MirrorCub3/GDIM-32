using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
[CreateAssetMenu(menuName = "Enemy/VisitorData")]
public class VisitorData : ScriptableObject
{
    [Header("Visit State")]
    public float visitTime;
}
