using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Composite Class
public class RatingManager : MonoBehaviour, Quality
{
    [SerializeField] private RestaurantData restaurantData;
    [SerializeField] private DessertSpawner dessertSpawn; // script to take list of dessert prefabs from
    float averageRating;

    [Header("InGameSliders")]
    public Slider StarSlider3;
    public Slider StarSlider2;
    public Slider StarSlider1;

    [Header("EndScreenSliders")]
    public Slider StarSlider3End;
    public Slider StarSlider2End;
    public Slider StarSlider1End;

    float currQuality;

    void Start()
    {
        averageRating = 0f;

        currQuality = 0f;

        StarSlider3.value = 0f;
        StarSlider2.value = 0f;
        StarSlider1.value = 0f;
    }

    void Update()
    {
        currQuality = GetQuality(); // current quality of all the desserts = the quality of your restaurant
        
        if (currQuality <= 1f){
            StarSlider1.value = currQuality;
            StarSlider2.value = 0f;
            StarSlider3.value = 0f;
        }
        else if (1f < currQuality && currQuality <= 2f){
            StarSlider1.value = 1f;
            StarSlider2.value = currQuality-1;
            StarSlider3.value = 0f;
        }
        else if (2f < currQuality && currQuality <= 3f){
            StarSlider1.value = 1f;
            StarSlider2.value = 1f;
            StarSlider3.value = currQuality-2;
        }
        else if (currQuality == 3f){
            StarSlider1.value = 1f;
            StarSlider2.value = 1f;
            StarSlider3.value = 1f;
        }
    }

    public float GetQuality(){
        averageRating = 0f; // added for testing purposes
        foreach (DessertController dessert in dessertSpawn.desserts)
            {
                averageRating += dessert.GetQuality();
            }
        return (averageRating/(dessertSpawn.desserts.Count-1));
    }

    public void SetEndSliders(){
        Update();
        StarSlider1End.value = StarSlider1.value;
        StarSlider2End.value = StarSlider2.value;
        StarSlider3End.value = StarSlider3.value;
        restaurantData.SetStars(currQuality);
    }
}
