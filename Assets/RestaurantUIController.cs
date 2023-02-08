using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RestaurantUIController : MonoBehaviour
{
    [SerializeField] private RestaurantData restaurantData;
    // always takes the Cookie Scriptable Objects right now
    public Sweets cookieSweet;

    // text to change during run-time
    public GameObject quantityOfDessert;
    TextMeshProUGUI textmeshpro_dessertQuantity;
    private int dessertQuantity;

    public GameObject moneyPerDessert;
    TextMeshProUGUI textmeshpro_dessertsMoney;
    private int dessertsMoney;

    void Start()
    {
        quantityOfDessert = this.transform.Find("Quantity of Dessert Text").gameObject;
        textmeshpro_dessertQuantity = quantityOfDessert.GetComponent<TMPro.TextMeshProUGUI>();
        textmeshpro_dessertQuantity.text = "0"; // quantity always starts off at 0

        moneyPerDessert = this.transform.Find("Money per Dessert Text").gameObject;
        textmeshpro_dessertsMoney = moneyPerDessert.GetComponent<TMPro.TextMeshProUGUI>();
        textmeshpro_dessertsMoney.text = "0"; // money per dessert always starts off at 0, and will return to 0 when there is no stock
    }

    void Update()
    {
        textmeshpro_dessertQuantity.text = restaurantData.stock.ToString(); // set the UI to display the current stock

        if (restaurantData.stock != 0){
            textmeshpro_dessertsMoney.text = cookieSweet.price.ToString();
        }
        else {
            textmeshpro_dessertsMoney.text = "0";
        }
    }
}
