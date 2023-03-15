using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Jessica Lam & Naman Khurana

public class RestaurantControl : MonoBehaviour
{

    //public KeyCode loadKey; // The key that player one must press to load the location
    //public Transform playerTwo; // The transform of player two
    //private bool canEnterLocation = false;


    public delegate void RestaurantNotif(string msg);
    public static RestaurantNotif OnError; // used to trigger popup when issue occurs

    [SerializeField] private InventoryData inventoryData;
    [SerializeField] private RestaurantData restaurantData;
    [SerializeField] private int requiredCount = 2; // the amount of players required to be present
    string kitchenLevel;

    // PlayerInteract references these, so the variables must be public
    public Image player1Bar;
    public CanvasGroup keyboardIcon1;
    public GameObject barBG1;


    bool calledLoad;
    int playerCount;
    public Sweets cookieSweet;


    // Start is called before the first frame update
    void Start()
    {
        // cannot see bar unless press key, sets up UI to be invisible or half visible
        barBG1.SetActive(false);
        keyboardIcon1.alpha = 0.5f;
        player1Bar.fillAmount = 0;
        // takes the gameobjects name (which player # the script is on)
        kitchenLevel = gameObject.name + "Kitchen";
        calledLoad = false;
        playerCount = 0;
    }



    private void OnEnable()
    {
        player1Bar.fillAmount = 0;
        calledLoad = false;
        barBG1.SetActive(false);
        playerCount = 0;
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform == playerTwo)
    //    {
    //        // If player two enters the trigger area, enable entering the location
    //        canEnterLocation = true;
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.transform == playerTwo)
    //    {
    //        // If player two leaves the trigger area, disable entering the location
    //        canEnterLocation = false;
    //    }
    //}

    void Update()
    {
        if (player1Bar.fillAmount == 1 && !calledLoad)
        {
            if (inventoryData.itemDictionary.TryGetValue(cookieSweet, out InventoryItem item)) // don't load if there is none of this item
            {
                if (item.stackSize <= 0)
                {
                    if (OnError != null)
                        OnError.Invoke("You have no " + cookieSweet.sweetName + " cards in stock.");
                    return;
                }
                else if(playerCount >= requiredCount)
                    LoadKitchen();
            }
            else
            {
                if (OnError != null)
                    OnError.Invoke("You haven't collected " + cookieSweet.sweetName + " cards yet.");
            }

        }
    }

    void LoadKitchen()
    {
        // Code to load the location
        GameManager.instance.LoadKitchen(kitchenLevel);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount += 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount -= 1;
        }
    }
}