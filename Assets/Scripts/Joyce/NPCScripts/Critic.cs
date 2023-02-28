using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class Critic : Eater
{
    [Header("Critic")]
    [SerializeField] private float starRemovalRate = .5f;
    public override void Eat(Restaurant restaurant)
    {
        int amountBought = restaurant.BuyProduct(Mathf.Min(myData.FeedRate(), (int)(hungerSlider.maxValue - hungerSlider.value))); // buys less than the npc's feed rate if there's not enough room
        hungerSlider.value += amountBought;
        chewTime = myData.ChewSpeed() * amountBought;

        if (amountBought <= 0) // if unsuccessful purchase
        {
            if (target == lastTarget)
            {
                print("eating nothing");
                emptyCount++;
            }
            else
            {
                lastTarget = target;
                emptyCount = 1;
            }
            anim.SetTrigger("Wander");
            if(restaurant.GetData().open)
                restaurant.RemoveStars(starRemovalRate);
        }
        else
            anim.SetTrigger("Eat");
    }
}
