using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//JESSICA Lam wrote this
public class RestaurantController : MonoBehaviour
{
    [SerializeField] private InventoryData inventoryData;
    private string kitchenLevel;

    public Image player1Bar;
    public Image player2Bar;

    public CanvasGroup keyboardIcon1;
    public CanvasGroup keyboardIcon2;

    public GameObject barBG1;
    public GameObject barBG2;

    private bool calledLoad;

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
        if (player1Bar.fillAmount == 1 && player2Bar.fillAmount == 1 && !calledLoad && inventoryData.itemDictionary[cookieSweet].stackSize > 0) // don't load if you don't have any cookies
        {
            print(player1Bar.fillAmount + " " + player2Bar.fillAmount + " " + !calledLoad);
            LoadKitchen();
        }
    }

    private void LoadKitchen()
    {
        calledLoad = true;
        Debug.Log("The scene: " + kitchenLevel + " should load");
        GameManager.instance.LoadKitchen(kitchenLevel);
    }
}
