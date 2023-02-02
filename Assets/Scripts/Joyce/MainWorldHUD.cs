using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class MainWorldHUD : MonoBehaviour
{
    [Header("Inventory Tab")]
    [SerializeField] private KeyCode inventoryToggleKey; // reference to the key that toggles inventory tab
    [SerializeField] private Animator inventoryTabAnim; // reference to recipe tab animator
    private bool inventoryOpen; // bool to toggle the tab being open

    //[Header("Restaurants Tab")] move code for the restaurant tab here
    void Update()
    {
        if (Input.GetKeyDown(inventoryToggleKey)) // allows for keypress to toggle
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory() // made public so that it can be toggled with button
    {
        inventoryOpen = !inventoryOpen;
        inventoryTabAnim.SetBool("Open", inventoryOpen);
    }
}
