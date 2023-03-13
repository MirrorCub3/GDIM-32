using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class RestaurantList : MonoBehaviour
{
    private static Dictionary<Sweets, Transform> restaurantLocs = new Dictionary<Sweets, Transform>();

    private void Awake() // super expensive! only do once
    {
        Restaurant[] restaurants = GameObject.FindObjectsOfType<Restaurant>();
        foreach(Restaurant r in restaurants)
        {
            restaurantLocs.Add(r.GetProduct(), r.transform);
        }
    }

    public Transform GetLocation(Sweets sweet)
    {
        if (restaurantLocs.TryGetValue(sweet, out Transform loc))
            return loc;
        else
        {
            Debug.Log("No restaurant serves " + sweet);
            return null;
        }
    }
}
