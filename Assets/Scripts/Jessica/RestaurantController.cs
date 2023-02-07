using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//JESSICA Lam wrote this
public class RestaurantController : MonoBehaviour
{
    private string kitchenLevel;

    public Image player1Bar;
    public Image player2Bar;

    public CanvasGroup keyboardIcon1;
    public CanvasGroup keyboardIcon2;

    public GameObject barBG1;
    public GameObject barBG2;

    [SerializeField] private float enterTimeCooldown = 2;
    private bool calledLoad;
    private bool canEnter;

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
        StartCoroutine(EnterCoolDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (player1Bar.fillAmount == 1 && player2Bar.fillAmount == 1 && !calledLoad)
        {
            print(player1Bar.fillAmount + " " + player2Bar.fillAmount + " " + !calledLoad);
            LoadKitchen();
        }
    }

    private void LoadKitchen()
    {
        if (!canEnter)
        {
            return;
        }

        calledLoad = true;
        Debug.Log("The scene: " + kitchenLevel + " should load");
        GameManager.instance.LoadKitchen(kitchenLevel);
    }

    private IEnumerator EnterCoolDown() // counters issue where player's are immediatly reloaded back into the kitchen
    {
        canEnter = false;
        yield return new WaitForSeconds(enterTimeCooldown);
        canEnter = true;
    }
}
