using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Has the list of Dessert Qualitys
public class DessertSpawner : MonoBehaviour
{   
    // Spawn Dessert
    float Timer = 2f;
    public GameObject dessertPrefab;
    GameObject dessertClone;

    // List of Dessert
    public List<Quality> desserts;

    // Take Kitchen Manager Script
    public KitchenManager kitchenManager;

    void Start()
    {
        desserts = new List<Quality>();
    }

    public void AddDessert(Quality dessert)
    {
        desserts.Add(dessert);
    }

    void Update()
    {
        // Creates a new dessert every 2 seconds
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            dessertClone = Instantiate(dessertPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            // HOW DO I ADD the dessertClone to a list of Quality? Since right now it's a gameobject ;-;

            if (kitchenManager){ // testing purposes
                kitchenManager.ReduceDessertByOne(); // call function from kitchen manager script to reduce dessert by one
            }
            Timer = 2f;
        }
    }


}
