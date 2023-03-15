using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class RestaurantManager : MonoBehaviour, IReset
{
    public delegate void OnCloseRestaurant(RestaurantData data);
    public event OnCloseRestaurant OnClose;

    [SerializeField] private List<RestaurantData> restaurants;
    private int openRestaurants = 0;

    private void Awake()
    {
        Reset();
    }

    public void Reset()
    {
        openRestaurants = 0;
        foreach (RestaurantData r in restaurants)
        {
            r.Reset();

            if(r.open)
                openRestaurants++;
        }
    }

    public void CloseRestaurant(RestaurantData data)
    {
        openRestaurants--;
        print(data + "just closed ");
        if (OnClose != null)
            OnClose.Invoke(data);
        if(openRestaurants <= 0)
        {
            GameManager.instance.GoToEndScreen();
        }
    }
    public void OpenRestaurant(RestaurantData restaurant)
    {
        restaurant.OpenCloseRestaurant(true);
        openRestaurants++;
    }

    private void OnDestroy()
    {
        Reset();
    }
}
