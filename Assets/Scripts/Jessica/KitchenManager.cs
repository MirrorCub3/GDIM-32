using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Jessica Lam
public class KitchenManager : MonoBehaviour
{
    [SerializeField] private InventoryData inventoryData;
    [SerializeField] private RestaurantData restaurantData;

    [SerializeField] private RatingManager ratingManager;
    [SerializeField] private DessertSpawner dessertSpawner;

    // UI Canvas
    [SerializeField] private GameObject BeforeGameCanvas;
    [SerializeField] private GameObject InGameCanvas;
    [SerializeField] private GameObject EndGameCanvas;

    private bool chosen;

    // text to change during run-time
    [SerializeField] private GameObject DessertsChosen;
    TextMeshProUGUI textmeshpro_dessertschosen;
    private int dessertsChosen;

    [SerializeField] private GameObject DessertsLeftDisplay;
    TextMeshProUGUI textmeshpro_dessertsleft;
    private int dessertsLeft;

    [SerializeField] private GameObject MaxCardsInventory;
    TextMeshProUGUI textmeshpro_maxcards;
    int maxCards;

    [SerializeField] private GameObject DessertsCreated;
    TextMeshProUGUI textmeshpro_dessertscreated;

    [SerializeField] private Sweets sweet;

    bool startTimeScale;

    // audio control
    AudioSource audioSource;

    void Start()
    {   // pause background and display the dessert choosing Canvas
        Time.timeScale = 0f;
        chosen = false;
        BeforeGameCanvas.SetActive(true);
        InGameCanvas.SetActive(false);
        EndGameCanvas.SetActive(false);
        textmeshpro_dessertschosen = DessertsChosen.GetComponent<TMPro.TextMeshProUGUI>();
        textmeshpro_dessertsleft = DessertsLeftDisplay.GetComponent<TMPro.TextMeshProUGUI>();
        textmeshpro_dessertscreated = DessertsCreated.GetComponent<TMPro.TextMeshProUGUI>();

        // set up the max amount of cards available (based on your inventory)
        textmeshpro_maxcards = MaxCardsInventory.GetComponent<TMPro.TextMeshProUGUI>();
        if (inventoryData.itemDictionary.TryGetValue(sweet, out InventoryItem item))
            textmeshpro_maxcards.text = (inventoryData.itemDictionary[sweet].stackSize).ToString();
        else
        {
            textmeshpro_maxcards.text = 0.ToString();
            Debug.Log("There are none of this card to cook");
        }

        // audio control initialize
        audioSource = GetComponent<AudioSource>();

        startTimeScale = false;
    }

    void Update()
    {   // once chose the amt of desserts, constantly update the text with the amount of desserts left
        if (chosen){
            textmeshpro_dessertsleft.text = dessertsLeft.ToString();
            startTimeScale = true;
        }
        else if (startTimeScale == false)
        {
            Time.timeScale = 0f;
        }
    }

    public void StartGameCycle(){
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play("Confirm");
        
        // start music
        audioSource.clip = sweet.BGMusic;
        audioSource.Play();

        dessertsChosen = int.Parse(textmeshpro_dessertschosen.text);
        textmeshpro_dessertscreated.text = dessertsChosen.ToString();
        
        inventoryData.Remove(sweet, dessertsChosen); // remove the desserts from the inventory
        restaurantData.AddStock(dessertsChosen); // add it to the restaurant stock (in outer world)

        dessertsLeft = dessertsChosen; // initialize the amount of desserts left
        chosen = true;

        // bring in the game canvas and remove the choosing-dessert canvas
        BeforeGameCanvas.SetActive(false);
        InGameCanvas.SetActive(true);
    }

    public void ReduceDessertByOne(){
        if (dessertsLeft == 0)
        {
            EndGameCycle();
        }
        else {
            dessertsLeft -= 1;
            textmeshpro_dessertsleft.text = dessertsLeft.ToString();
        }
    }

    void EndGameCycle(){
        Time.timeScale = 0f;
        audioSource.Stop();
        ratingManager.SetEndSliders();
        EndGameCanvas.SetActive(true);
    }

    // used for player input at the end of kitchen
    public void BackToOuterWorld(){
        dessertSpawner.DestroyAllDesserts();
        Time.timeScale = 1f;
        GameManager.instance.UnloadKitchen();
    }
}
