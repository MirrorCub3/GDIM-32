using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Emily Chavez
public class RestaurantBuyButton : MonoBehaviour
{
    [Header("Restuarant Info")]
    [SerializeField] private RestaurantData product; // this is a reference to the restaurant object that this button purchases
    [SerializeField] private int cost;
    private RestaurantManager manager;
    

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private GameObject buttonGO;
    [SerializeField] private Button myButton;
    [SerializeField] private GameObject cardButton;
    [SerializeField] private GameObject dirtButton;

    private void Awake()
    {
        manager = FindObjectOfType<RestaurantManager>();
        manager.OnClose += Closed;
        costText.text = cost.ToString();
        myButton.interactable = false;
        if (product.open)
            Open();
        else
            Closed(product);
    }

    private void Update(){
        CheckCanUse(RevenueManager.instance.coins); // checks if the button can be used based on current revenue
    }

    private void OnEnable()
    {
        RevenueManager.CoinsChanged += CheckCanUse;
        manager.OnClose += Closed;
    }
    private void OnDisable()
    {
        RevenueManager.CoinsChanged -= CheckCanUse;
        manager.OnClose -= Closed;
    }

    private void CheckCanUse(int currCoins)
    {
        myButton.interactable = (currCoins >= cost);
    }

    public void Purchase() //subtract cost from rev 
    {
        manager.OpenRestaurant(product);
        RevenueManager.instance.ChangeCoins(-cost);
        Open();
    }
    private void Open()
    {
        buttonGO.SetActive(false);
        cardButton.SetActive(true);
        dirtButton.SetActive(true);

    }
    private void Closed(RestaurantData data)
    {
        if (product == data)
        {
            buttonGO.SetActive(true);
            cardButton.SetActive(false);
            dirtButton.SetActive(false);
        }

    }
}
