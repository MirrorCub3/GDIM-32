using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KitchenManager : MonoBehaviour
{
    // private string currentDessertRestaurant;
    // private int currentBPM;

    [SerializeField] private InventoryData inventoryData;

    // UI Canvas
    public GameObject BeforeGameCanvas;
    public GameObject InGameCanvas;

    bool chosen;

    // text to change during run-time
    public GameObject DessertsChosen;
    TextMeshProUGUI textmeshpro_dessertschosen;
    private int dessertsChosen;

    public GameObject DessertsLeftDisplay;
    TextMeshProUGUI textmeshpro_dessertsleft;
    private int dessertsLeft;

    public GameObject MaxCardsInventory;
    TextMeshProUGUI textmeshpro_maxcards;
    int maxCards;

    // always takes the Cookie Scriptable Objects right now
    public Sweets cookieSweet;

    void Start()
    {   // pause background and display the dessert choosing Canvas
        Time.timeScale = 0f;
        chosen = false;
        BeforeGameCanvas.SetActive(true);
        InGameCanvas.SetActive(false);
        textmeshpro_dessertschosen = DessertsChosen.GetComponent<TMPro.TextMeshProUGUI>();
        textmeshpro_dessertsleft = DessertsLeftDisplay.GetComponent<TMPro.TextMeshProUGUI>();
        
        // set up the max amount of cards available (based on your inventory)
        textmeshpro_maxcards = MaxCardsInventory.GetComponent<TMPro.TextMeshProUGUI>();
        textmeshpro_maxcards.text = (inventoryData.itemDictionary[cookieSweet].stackSize).ToString();
    }

    void Update()
    {   // once chose the amt of desserts, constantly update the text with the amount of desserts left
        if (chosen){
            textmeshpro_dessertsleft.text = dessertsLeft.ToString();
        }
    }

    public void StartGameCycle(){
        // start the rhythm kitchen section after choosing the amount of desserts
        Time.timeScale = 1f;
        dessertsChosen = int.Parse(textmeshpro_dessertschosen.text);
        
        inventoryData.Remove(cookieSweet, dessertsChosen); // remove the desserts from the inventory

        dessertsLeft = dessertsChosen; // initialize the amount of desserts left
        chosen = true;

        // bring in the game canvas and remove the choosing-dessert canvas
        BeforeGameCanvas.SetActive(false);
        InGameCanvas.SetActive(true);
    }
}
