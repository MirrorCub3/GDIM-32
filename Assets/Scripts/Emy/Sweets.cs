using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Sweets : ScriptableObject
{
   public string SweetName;
   public Sprite icon;
   
   public int valueDepletionMult;
   public int MaxRev;
   public int PlantGrowthTime;

}
