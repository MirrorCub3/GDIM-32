using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jessica Lam
public class RestaurantUI : MonoBehaviour
{
    [SerializeField] private Restaurant restaurant;
    [SerializeField] GameObject restaurantDisplay;
    int playerCount;

    // Start is called before the first frame update
    void Start()
    {
        restaurantDisplay.SetActive(false);
        playerCount = 0;
    }

    private void OnEnable()
    {
        playerCount = 0;
    }

    private void Update()
    {
        if(!restaurant.IsOpen())
            restaurantDisplay.SetActive(false);
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player") && restaurant.IsOpen())
        {
            restaurantDisplay.SetActive(true);
            playerCount += 1;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player") && restaurant.IsOpen())
        {
            playerCount -= 1;
            if (playerCount == 0){
                restaurantDisplay.SetActive(false);
            }
        }
    }
}
