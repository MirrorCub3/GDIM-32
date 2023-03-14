using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class Restaurant : MonoBehaviour, IReset
{
    [Header("Data")]
    [SerializeField] private RestaurantData myData;
    [SerializeField] private Sweets product;
    [SerializeField] private PlantingPatch dirt;
    [SerializeField] private bool startOpen;
    [SerializeField] private RestaurantOpenSpawner spawner;

    [Header("Visual")]
    [SerializeField] private GameObject lockIcon;
    public int stock { get; private set; }

    private void Awake()
    {
        stock = 0;
        myData.Init(stock, startOpen);
        if (startOpen)
            Open();
    }


    private void OnEnable() // when entering back into the scene, match data with scriptable object
    {
        stock = myData.stock;
        myData.OnOpenClose += OnOpenClose;
    }

    private void OnDisable()
    {
        myData.OnOpenClose -= OnOpenClose;
    }
    public bool IsOpen()
    {
        return myData.open;
    }

    public Sweets GetProduct()
    {
        return product;
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
            Close();
        }
    }

    private void Close()
    {
        if (!myData.open)
            return;
        lockIcon.SetActive(true);
        RestaurantManager.instance.CloseRestaurant(myData);
        if (dirt)
            dirt.Unlock(PlantingPatch.DirtStates.LOCKED);
    }

    private void Open()
    {
        lockIcon.SetActive(false);
        if (dirt)
            dirt.Unlock();
        // do the popup if it wasnt opened before
        if (!myData.wasOpened && spawner)
        {
            spawner.Spawn();
            myData.SetWasOpened();
        }
    }

    private void OnOpenClose(bool isOpen)
    {
        if (isOpen)
            Open();
        else
            Close();
    }

    public RestaurantData GetData()
    {
        return myData;
    }

    public void Reset()
    {
        myData.Reset();
        stock = 0;
    }
}
