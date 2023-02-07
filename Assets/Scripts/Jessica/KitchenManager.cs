using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KitchenManager : MonoBehaviour
{
    // private string currentDessertRestaurant;
    // private int currentBPM;

    [SerializeField] private InventoryData inventoryData;

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

    // always takes the Cookie Scriptable Objects
    public Sweets cookieSweet;

    void Start()
    {
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
    {
        if (chosen){
            textmeshpro_dessertsleft.text = dessertsLeft.ToString();
        }
    }

    public void StartGameCycle(){
        Time.timeScale = 1f;
        dessertsChosen = int.Parse(textmeshpro_dessertschosen.text);

        dessertsLeft = dessertsChosen;
        chosen = true;

        BeforeGameCanvas.SetActive(false);
        InGameCanvas.SetActive(true);
    }
}
