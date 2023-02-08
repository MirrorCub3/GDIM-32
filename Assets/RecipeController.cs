using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecipeController : MonoBehaviour
{    
    [SerializeField] private RestaurantData restaurantData;
    // always takes the Cookie Scriptable Objects right now
    public Sweets cookieSweet;

    // cookie text to change during run-time
    GameObject quantityOfCookie;
    TextMeshProUGUI textmeshpro_cookieQuantity;

    GameObject moneyPerCookie;
    TextMeshProUGUI textmeshpro_cookieMoney;

    void Start()
    {
        quantityOfCookie = this.transform.Find("CookieNum Text").gameObject;
        textmeshpro_cookieQuantity = quantityOfCookie.GetComponent<TMPro.TextMeshProUGUI>();

        moneyPerCookie = this.transform.Find("Coin per cookie Text").gameObject;
        textmeshpro_cookieMoney = moneyPerCookie.GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        textmeshpro_cookieQuantity.text = restaurantData.stock.ToString(); // set the UI to display the current stock

        if (restaurantData.stock != 0){
            textmeshpro_cookieMoney.text = cookieSweet.price.ToString();;
        }
        else {
            textmeshpro_cookieMoney.text = "0";
        }

    }
}
