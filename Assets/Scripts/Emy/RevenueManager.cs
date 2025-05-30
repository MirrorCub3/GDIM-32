using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Emily Chavez and Joyce Mai
public class RevenueManager : MonoBehaviour, IReset
{ 
    // allows subscription to knowing when coins has changed
    public delegate void OnChangedCoins(int amount);
    public static event OnChangedCoins CoinsChanged;

    public static RevenueManager instance;
    public int coins { get; private set; }

    [SerializeField] private int startCoins = 0;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this);
            return;
        }

        Reset();
    }

    // method to change number of coins
    public void ChangeCoins(int changeCoins)
    {
        if (coins + changeCoins < 0)
        {
            Debug.Log("Insufficient funds, how are u doing this???");
            return;
        }

        coins += changeCoins;

        // updating subcribers that coins has changed
        if (CoinsChanged != null)
            CoinsChanged.Invoke(coins);
    }

    public void Reset()
    {
        coins = startCoins;
    }
}
