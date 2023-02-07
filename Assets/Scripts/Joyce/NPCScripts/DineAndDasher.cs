using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DineAndDasher : Visitor
{
    [Header("Dine N' Dash")]
    [SerializeField] private int theftAmount;

    public override void AtRestaurant(Restaurant restaurant)
    {
        base.AtRestaurant(restaurant);
        if(restaurant != null)
            Steal(restaurant);
    }

    private void Steal(Restaurant restaurant)
    {
        Debug.Log("HEHE STEALING " + theftAmount + " OF YOUR FOOD");
        Debug.Log("thats rough buddy");
        restaurant.StealProduct(theftAmount);
    }
}
