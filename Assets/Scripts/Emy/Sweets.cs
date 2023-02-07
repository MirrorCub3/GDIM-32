using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Emily Chavez
[CreateAssetMenu]
public class Sweets : ScriptableObject
{
    [Header("Visuals")]
    public string sweetName;
    public Sprite icon;
    public Sprite UIIcon;
    public List<Sprite> qualityIcons;

    [Header("Number Values")]
    public float growthTime;

}
