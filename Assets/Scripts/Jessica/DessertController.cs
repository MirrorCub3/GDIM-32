using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DessertController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When this dessert reaches the perfect placement window
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Placement"))
        {
            Debug.Log("I'm in the window of placement!");
        }
    }

    // When this dessert exits the perfect placement window
    void OnTriggerExit2D (Collider2D other)
    {
        if (other.CompareTag("Placement"))
        {
            Debug.Log("I have left the window!");
        }
    }
}
