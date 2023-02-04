using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
[CreateAssetMenu(menuName = "PersistentData/ RestaurantData")]
public class RestaurantData : ScriptableObject
{
    // will hold persistent restaurant data
    public bool open { get; private set; }
    public float stars;

}
