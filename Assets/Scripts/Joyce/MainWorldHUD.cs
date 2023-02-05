using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai - Inventory toggle
//Emily Chavez - recipe toggle 
public class MainWorldHUD : MonoBehaviour
{
    [Header("Inventory Tab")]
    [SerializeField] private KeyCode inventoryToggleKey; // reference to the key that toggles inventory tab
    [SerializeField] private Animator inventoryTabAnim; // reference to recipe tab animator
    private bool inventoryOpen; // bool to toggle the tab being open

    [Header("Restaurants Tab")]
    [SerializeField] private GameObject UIrecipeMenu;
    private bool _recipeMenu = false;

    void Update()
    {
        if (Input.GetKeyDown(inventoryToggleKey)) // allows for keypress to toggle
        {
            ToggleInventory();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_recipeMenu)
            {
                Resume();
            }
            else
            {
                OpenRecipeMenu();
            }
        }
    }

    public void ToggleInventory() // made public so that it can be toggled with button
    {
        inventoryOpen = !inventoryOpen;
        inventoryTabAnim.SetBool("Open", inventoryOpen);
    }

     public void Resume()
    {
        UIrecipeMenu.SetActive(false);
        _recipeMenu = false;
    }

    private void OpenRecipeMenu()
    {
        UIrecipeMenu.SetActive(true);
        _recipeMenu = true;
    }

    public void ToggleRecipeMenu()
    {
        if (UIrecipeMenu.activeInHierarchy)
            Resume();
            
        else
           {OpenRecipeMenu();
           }
    }

}
