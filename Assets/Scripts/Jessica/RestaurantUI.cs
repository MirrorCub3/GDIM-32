using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jessica Lam

public class RestaurantUI : MonoBehaviour
{
    public GameObject restaurantDisplay;

    // Start is called before the first frame update
    void Start()
    {
        restaurantDisplay.SetActive(false);
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            restaurantDisplay.SetActive(true);
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            restaurantDisplay.SetActive(false);
            
        }
    }
}
