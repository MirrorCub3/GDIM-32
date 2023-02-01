using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Jessica Lam
public class PlayerInteract : MonoBehaviour
{
    // which kitchen are we next to (the scene name must be the name of the object + Restaurant)
    private string kitchenLevel;

    private string currentPlayer;
    string keyPressCode;

    private float startPressTime = 0f;
    private bool playerInRange;
    private bool holdingButton;

    private CanvasGroup keyboardIcon;
    private GameObject barBG;

    // take respective BAR for the restaurant we're at
    public RestaurantController restaurantController;
    private Image playerBar;

    // Start is called before the first frame update
    void Start()
    {
        // starts off setting to false
        playerInRange = false;
        // takes the gameobjects name (which player # the script is on)
        currentPlayer = gameObject.name;

        // set different key bind for each player
        if (currentPlayer == "Player1"){
            keyPressCode = "e";
        }
        else if (currentPlayer == "Player2"){
            keyPressCode = "p";
        } 
    }

    // called every frame
    void Update()
    {
        if (holdingButton == false){
            startPressTime = Time.time;
        }

        // when press keyPressCode and near the restaurant, set the start press time
        if (Input.GetKeyDown(keyPressCode) && playerInRange == true)
        {
            holdingButton = true;
            startPressTime = Time.time;
            barBG.SetActive(true);
        }

        // increase the fill progress bar if holding the button
        if (holdingButton){
            playerBar.fillAmount = Time.time - startPressTime;
        }

        // if keyPressCode lifted
        if (Input.GetKeyUp(keyPressCode) && playerInRange == true)
        {
            holdingButton = false;
            playerBar.fillAmount = 0;
            barBG.SetActive(false);
        }
    }

    // When this player goes near a restaurant
    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Restaurant"))
        {
            // Takes necessary information from the restaurant we're at
            restaurantController = other.GetComponent<RestaurantController>();
            
            if (currentPlayer == "Player1"){
                playerBar = restaurantController.player1Bar;
                keyboardIcon = restaurantController.keyboardIcon1;
                barBG = restaurantController.barBG1;
            }
            else if (currentPlayer == "Player2"){
                playerBar = restaurantController.player2Bar;
                keyboardIcon = restaurantController.keyboardIcon2;
                barBG = restaurantController.barBG2;
            }

            playerInRange = true;
            kitchenLevel = other.gameObject.name + " Restaurant";
            // sets the icon alpha levels
            keyboardIcon.alpha = 1f;
        }
    }

    // When this player leaves the restaurant trigger
    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Restaurant"))
        {
            playerInRange = false;
            kitchenLevel = "";
            // sets the icon alpha levels
            keyboardIcon.alpha = 0.5f;
        }
    }
}
