using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingController : MonoBehaviour
{   
    // Spawn Dessert
    float Timer = 2f;
    public GameObject dessertPrefab;
    GameObject dessertClone;

    // Take Kitchen Manager Script
    public KitchenManager kitchenManager;

    // Checking how many desserts were made, update text

    void Start()
    {
        
    }

    void Update()
    {
        // Creates a new dessert every 2 seconds
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            dessertClone = Instantiate(dessertPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            if (kitchenManager){ // testing purposes
                kitchenManager.ReduceDessertByOne(); // call function from kitchen manager script to reduce dessert by one
            }
            Timer = 2f;
        }
    }
}
