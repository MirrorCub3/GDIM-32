using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeController : MonoBehaviour
{    
    [SerializeField] private RestaurantData restaurantData;
    public Sweets sweet;

    // cookie text to change during run-time
    GameObject quantityOfCookie;
    TextMeshProUGUI textmeshpro_cookieQuantity;

    GameObject moneyPerCookie;
    TextMeshProUGUI textmeshpro_cookieMoney;

    GameObject starSlider1GO;
    GameObject starSlider2GO;
    GameObject starSlider3GO;
    Slider starSlider1;
    Slider starSlider2;
    Slider starSlider3;

    float multiplier;

    void Start()
    {
        quantityOfCookie = this.transform.Find("CookieNum Text").gameObject;
        textmeshpro_cookieQuantity = quantityOfCookie.GetComponent<TMPro.TextMeshProUGUI>();

        moneyPerCookie = this.transform.Find("Coin per cookie Text").gameObject;
        textmeshpro_cookieMoney = moneyPerCookie.GetComponent<TMPro.TextMeshProUGUI>();

        starSlider1GO = this.transform.Find("StarSlider1").gameObject;
        starSlider1 = starSlider1GO.GetComponent<Slider>();
        starSlider1.value = 0f;
        starSlider2GO = this.transform.Find("StarSlider2").gameObject;
        starSlider2 = starSlider2GO.GetComponent<Slider>();
        starSlider2.value = 0f;
        starSlider3GO = this.transform.Find("StarSlider3").gameObject;
        starSlider3 = starSlider3GO.GetComponent<Slider>();
        starSlider3.value = 0f;

        multiplier = 1f;
    }

    void Update()
    {
        textmeshpro_cookieQuantity.text = restaurantData.stock.ToString(); // set the UI to display the current stock

        if (restaurantData.stock != 0){
            textmeshpro_cookieMoney.text = (sweet.price * multiplier).ToString();
        }
        else {
            textmeshpro_cookieMoney.text = "0";
            //SetStarsToZero();
        }

        if (restaurantData.stars != 0f){
            SetStars();
        }
    }

    void SetStarsToZero(){
        restaurantData.SetStars(0f);
        starSlider1.value = 0f;
        starSlider2.value = 0f;
        starSlider3.value = 0f;
    }

    void SetStars(){
        if (restaurantData.stars <= 1f){
            starSlider1.value = restaurantData.stars;
            starSlider2.value = 0f;
            starSlider3.value = 0f;
            multiplier = .5f;
        }
        else if (1f < restaurantData.stars && restaurantData.stars <= 2f){
            starSlider1.value = 1f;
            starSlider2.value = restaurantData.stars-1;
            starSlider3.value = 0f;
            multiplier = .8f;
        }
        else if (2f < restaurantData.stars && restaurantData.stars <= 3f){
            starSlider1.value = 1f;
            starSlider2.value = 1f;
            starSlider3.value = restaurantData.stars-2;
            multiplier = 1f;
        }
        else if (restaurantData.stars == 3f){
            starSlider1.value = 1f;
            starSlider2.value = 1f;
            starSlider3.value = 1f;
            multiplier = 1f;
        }
    }
}
