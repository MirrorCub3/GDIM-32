using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class Restaurant : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private RestaurantData myData;
    [SerializeField] private Sweets product;
    public int stock { get; private set; }

    private void Awake()
    {
        stock = 0;
        myData.Init(stock);
    }

    private void OnEnable()
    {
        stock = myData.stock;
    }

    public int BuyProduct(int amount = 1) // returns true or false based on successful purchase
    {
        if (stock - amount < 0)
        {
            Debug.Log("Not enough stock");
            return 0;
        }

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
}
