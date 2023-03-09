using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Jessica Lam
public class RestaurantController : MonoBehaviour
{
    public delegate void RestaurantNotif(string msg);
    public static RestaurantNotif OnError; // used to trigger popup when issue occurs

    [SerializeField] private InventoryData inventoryData;
    [SerializeField] private RestaurantData restaurantData;
    string kitchenLevel;

    // PlayerInteract references these, so the variables must be public
    public Image player1Bar;
    public Image player2Bar;

    public CanvasGroup keyboardIcon1;
    public CanvasGroup keyboardIcon2;

    public GameObject barBG1;
    public GameObject barBG2;

    bool calledLoad;

    public Sweets cookieSweet;

    // Start is called before the first frame update
    void Start()
    {
        // cannot see bar unless press key, sets up UI to be invisible or half visible
        barBG1.SetActive(false);
        barBG2.SetActive(false);
        keyboardIcon1.alpha = 0.5f;
        keyboardIcon2.alpha = 0.5f;
        player1Bar.fillAmount = 0;
        player2Bar.fillAmount = 0;
        // takes the gameobjects name (which player # the script is on)
        kitchenLevel = gameObject.name + "Kitchen";
        calledLoad = false;
    }
    private void OnEnable()
    {
        player1Bar.fillAmount = 0;
        player2Bar.fillAmount = 0;
        calledLoad = false;
        barBG1.SetActive(false);
        barBG2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player1Bar.fillAmount == 1 && player2Bar.fillAmount == 1 && !calledLoad)
        {

            if (!restaurantData.open)
            {
                //if (OnError != null)
                    //OnError.Invoke("This restaurant is not open."); // jess commented out because i removed the UI restaurant display when restaurant isn't open, so it shouldn't do anything
                return;
            }

            if (inventoryData.itemDictionary.TryGetValue(cookieSweet, out InventoryItem item)) // don't load if there is none of this item
            {
                if (item.stackSize <= 0)
                {
                    if (OnError != null)
                        OnError.Invoke("You have no " + cookieSweet.sweetName + " cards in stock.");
                    return;
                }
                LoadKitchen();
            }
            else
            {
                if (OnError != null)
                    OnError.Invoke("You haven't collected " + cookieSweet.sweetName + " cards yet.");
            }
        }
    }

    private void LoadKitchen()
    {
        calledLoad = true;
        Debug.Log("The scene: " + kitchenLevel + " should load");
        GameManager.instance.LoadKitchen(kitchenLevel);
    }
}
