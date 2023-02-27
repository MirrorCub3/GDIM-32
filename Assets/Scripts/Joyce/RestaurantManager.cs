using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class RestaurantManager : MonoBehaviour, IReset
{
    public static RestaurantManager instance;

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

    public void CloseRestaurant()
    {
        openRestaurants--;
        if(openRestaurants <= 0)
        {
            GameManager.instance.GoToEndScreen(GameManager.GameState.LOSE);
        }
    }
    public void OpenRestaurant()
    {
        openRestaurants++;
    }

}
