using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critic : Eater
{
    public override void Eat(Restaurant restaurant)
    {
        int amountBought = restaurant.BuyProduct(Mathf.Min(myData.FeedRate(), (int)(hungerSlider.maxValue - hungerSlider.value))); // buys less than the npc's feed rate if there's not enough room
        hungerSlider.value += amountBought;
        chewTime = myData.ChewSpeed() * amountBought;

        if (amountBought <= 0) // if unsuccessful purchase
        {
            if (target == lastTarget)
            {
                print("eating nothing"); // remove stars
                emptyCount++;
            }
            else
            {
                lastTarget = target;
                emptyCount = 1;
            }
            anim.SetTrigger("Wander");

        }
        else
            anim.SetTrigger("Eat");
    }
}
