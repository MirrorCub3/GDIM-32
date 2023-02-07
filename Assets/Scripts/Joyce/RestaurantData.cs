using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
[CreateAssetMenu(menuName = "PersistentData/ RestaurantData")]
public class RestaurantData : ScriptableObject, IReset
{
    // will hold persistent restaurant data
    public bool open { get; private set; }
    public float stars { get; private set; }
    public int stock { get; private set; }

    public void Init(int startStock = 0)
    {
        stock = startStock;
    }

    public void Reset()
    {
        stock = 0;
        stars = 0;
        open = false;
    }

    public void AddStock(int amount)
    {
        stock += amount;
    }

    public void RemoveStock(int amount)
    {
        if (stock - amount < 0)
        {
            Debug.Log("Invalid Removal");
        }
        stock -= amount;
    }
}
