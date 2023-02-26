using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class Restaurant : MonoBehaviour, IReset
{
    [Header("Data")]
    [SerializeField] private RestaurantData myData;
    [SerializeField] private Sweets product;
    [SerializeField] private bool startOpen;
    public int stock { get; private set; }

    private void Awake()
    {
        stock = 0;
        myData.Init(stock, startOpen);
    }

    private void OnEnable() // when entering back into the scene, match data with scriptable object
    {
        stock = myData.stock;
    }

    public bool IsOpen()
    {
        return myData.open;
    }

    public int BuyProduct(int amount = 1) // returns true or false based on successful purchase
    {
        if (stock <= 0)
        {
            Debug.Log("There is no stock");
            return 0;
        }
        
        if (amount > stock) // if trying to steal more than avaliable, just take everything
            amount = stock;

        stock -= amount;
        RevenueManager.instance.ChangeCoins(product.price * amount);
        myData.RemoveStock(amount);
        return amount * product.hungerFillAmount;
    }

    public bool StealProduct( int amount)
    {
        if (stock <= 0)
        {
            Debug.Log("Can't steal if they're broke");
            return false;
        }

        if (amount > stock) // if trying to steal more than avaliable, just take everything
            amount = stock;

        stock -= amount;
        myData.RemoveStock(amount);
        return true;
    }

    public void RemoveStars(float removeAmount)
    {
        myData.RemoveStars(removeAmount);
        if(myData.stars <= 0)
        {
            RestaurantManager.instance.CloseRestaurant();
        }
    }

    public void Reset()
    {
        myData.Reset();
        stock = 0;
    }
}
