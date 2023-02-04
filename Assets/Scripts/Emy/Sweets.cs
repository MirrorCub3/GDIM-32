using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Emy
[CreateAssetMenu]
public class Sweets : ScriptableObject
{
    [Header("Visuals")]
    public string sweetName;
    public Sprite icon;

    [Header("Number Values")]
    public float growthTime;

}
