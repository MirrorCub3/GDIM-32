using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Has the list of Dessert Qualitys
public class DessertSpawner : MonoBehaviour
{   
    // Spawn Dessert
    float Timer = 2f;
    public DessertController dessertPrefab;
    DessertController dessertClone;

    // List of Dessert
    public List<DessertController> desserts;

    // Take Kitchen Manager Script
    public KitchenManager kitchenManager;

    void Start()
    {
        desserts = new List<DessertController>();
    }

    public void AddDessert(DessertController dessert)
    {
        desserts.Add(dessert);
    }

    void Update()
    {
        // Creates a new dessert every 2 seconds
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            dessertClone = Instantiate(dessertPrefab);
            desserts.Add(dessertClone);
            Debug.Log("Desserts in the list: " + desserts.Count);

            if (kitchenManager){ // testing purposes
                kitchenManager.ReduceDessertByOne(); // call function from kitchen manager script to reduce dessert by one
            }
            Timer = 2f;
        }
    }


}
