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

    [Header("Visuals")]
    [SerializeField] private Image productIcon;
    [SerializeField] private TextMeshProUGUI costText;

    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>(); // this is the reference the inventory to update/add to

        productIcon.sprite = product.icon; // change this to reference the UI icon instead
        // change the costtext to reflect the cost
    }

    public void Purchase() //subtract cost from rev 
    {
        inventory.Add(product, 1); // adding one of this sweets object to the inventory
    }
}
