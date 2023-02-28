using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class RestaurantManager : MonoBehaviour, IReset
{
    public static RestaurantManager instance;
    public delegate void OnCloseRestaurant(RestaurantData data);
    public static OnCloseRestaurant OnClose;

    [SerializeField] private List<RestaurantData> restaurants;
    private int openRestaurants = 0;

    private void Awake()
    {
        // singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
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
        if (OnClose != null)
            OnClose.Invoke(data);
        if(openRestaurants <= 0)
        {
            GameManager.instance.GoToEndScreen(GameManager.GameState.LOSE);
        }
    }
    public void OpenRestaurant(RestaurantData restaurant)
    {
        restaurant.OpenCloseRestaurant(true);
        openRestaurants++;
    }

}
