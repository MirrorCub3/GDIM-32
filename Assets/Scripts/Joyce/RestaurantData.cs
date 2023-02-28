using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
[CreateAssetMenu(menuName = "PersistentData/ RestaurantData")]
public class RestaurantData : ScriptableObject, IReset
{
    // will hold persistent restaurant data
    public bool open { get; private set; }
    private bool startOpen;
    public float stars { get; private set; }
    [SerializeField] private float startStars = 1.5f;
    public int stock { get; private set; }

    public void Init(int startStock = 0, bool isOpen = false)
    {
        stock = startStock;
        stars = startStars;
        open = isOpen;
        startOpen = open;
    }

    public void Reset()
    {
        stock = 0;
        stars = startStars;
        open = startOpen;
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

    public void SetStars(float amount) // call to set new quality for restaurant in kitchen
    {
        stars = Mathf.Min(3, amount);
        stars = Mathf.Max(0, stars);
    }
    public void RemoveStars(float amount) // call to set new quality for restaurant in kitchen
    {
        stars = Mathf.Max(0, stars - amount);
    }

    public void OpenCloseRestaurant(bool isOpen)
    {
        open = isOpen;
    }
}
