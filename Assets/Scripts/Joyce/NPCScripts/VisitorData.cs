using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
[CreateAssetMenu(menuName = "Enemy/VisitorData")]
public class VisitorData : ScriptableObject
{
    public RangeInt visitTime { get; private set; }
}
