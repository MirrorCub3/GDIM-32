using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Joyce Mai - Inventory toggle
//Emily Chavez - Recipe toggle 
public class MainWorldHUD : MonoBehaviour
{
    [Header("Inventory Tab")]
    [SerializeField] private KeyCode inventoryToggleKey; // reference to the key that toggles inventory tab
    [SerializeField] private Animator inventoryTabAnim; // reference to recipe tab animator
    private bool inventoryOpen; // bool to toggle the tab being open

    [Header("Restaurants Tab")]
    [SerializeField] private GameObject UIrecipeMenu;
    private bool _recipeMenu = false;

    [Header("Money")]
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("Popup")]
    [SerializeField] private GameObject popup;
    [SerializeField] private TextMeshProUGUI messageText;

    private void Awake()
    {
        Resume();
        UpdateCoinText(RevenueManager.instance.coins);
        ClosePopup();
    }

    private void OnEnable()
    {
        RevenueManager.CoinsChanged += UpdateCoinText;
        RestaurantController.OnError += PopUpMessage;
    }
    private void OnDisable()
    {
        RevenueManager.CoinsChanged -= UpdateCoinText;
        RestaurantController.OnError -= PopUpMessage;
    }

    void Update()
    {
        if (Input.GetKeyDown(inventoryToggleKey)) // allows for keypress to toggle
        {
            ToggleInventory();
        }

        if (Input.GetKeyDown(KeyCode.R)) // keypress toggles recipe display
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
            OpenRecipeMenu();
    }

    private void UpdateCoinText(int amount)
    {
        coinText.text = amount.ToString();
    }

    private void PopUpMessage(string msg)
    {
        popup.SetActive(true);
        messageText.text = msg;
    }
    public void ClosePopup()
    {
        popup.SetActive(false);
    }
}
