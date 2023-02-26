using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RestaurantUIController : MonoBehaviour
{
    [SerializeField] private RestaurantData restaurantData;
    public Sweets sweet;

    // text to change during run-time
    GameObject quantityOfDessert;
    TextMeshProUGUI textmeshpro_dessertQuantity;
    private int dessertQuantity;

    GameObject moneyPerDessert;
    TextMeshProUGUI textmeshpro_dessertsMoney;
    private int dessertsMoney;

    GameObject starSlider1GO;
    GameObject starSlider2GO;
    GameObject starSlider3GO;
    Slider starSlider1;
    Slider starSlider2;
    Slider starSlider3;

    float multiplier;

    void Start()
    {
        quantityOfDessert = this.transform.Find("Quantity of Dessert Text").gameObject;
        textmeshpro_dessertQuantity = quantityOfDessert.GetComponent<TMPro.TextMeshProUGUI>();
        textmeshpro_dessertQuantity.text = "0"; // quantity always starts off at 0

        moneyPerDessert = this.transform.Find("Money per Dessert Text").gameObject;
        textmeshpro_dessertsMoney = moneyPerDessert.GetComponent<TMPro.TextMeshProUGUI>();
        textmeshpro_dessertsMoney.text = "0"; // money per dessert always starts off at 0, and will return to 0 when there is no stock

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
        textmeshpro_dessertQuantity.text = restaurantData.stock.ToString(); // set the UI to display the current stock

        if (restaurantData.stock != 0){
            textmeshpro_dessertsMoney.text = (sweet.price * multiplier).ToString();
        }
        else {
            textmeshpro_dessertsMoney.text = "0";
            //SetStarsToZero(); // commented this out to allow restaurants to start at 3 stars
        }

        //if (restaurantData.stars != 0f)
        //{
        //    SetStars();
        //}
        SetStars();
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
