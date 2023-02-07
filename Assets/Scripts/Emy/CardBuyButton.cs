using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Emily Chavez
public class CardBuyButton : MonoBehaviour
{
    [Header("Item Info")]
    [SerializeField] private Sweets product; // this is a reference to the sweets object that this button purchases
    [SerializeField] private int cost;

    [Header("UI")]
    [SerializeField] private Image productIcon;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button myButton;

    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>(); // this is the reference the inventory to update/add to
        productIcon.sprite = product.icon; // change this to reference the UI icon instead
        costText.text = cost.ToString();

        CheckCanUse(RevenueManager.instance.coins); // checks if the button can be used based on current revenue
    }

    private void OnEnable()
    {
        RevenueManager.CoinsChanged += CheckCanUse;
    }
    private void OnDisable()
    {
        RevenueManager.CoinsChanged -= CheckCanUse;
    }

    private void CheckCanUse(int currCoins)
    {
        myButton.interactable = currCoins >= cost;
    }

    public void Purchase() //subtract cost from rev 
    {
        inventory.Add(product, 1); // adding one of this sweets object to the inventory
        RevenueManager.instance.ChangeCoins(-cost);
    }
}
