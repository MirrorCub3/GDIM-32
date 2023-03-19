using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Jessica Lam
// Composite Class
public class RatingManager : MonoBehaviour, Quality
{
    [SerializeField] private RestaurantData restaurantData;
    [SerializeField] private DessertSpawner dessertSpawn; // script to take list of dessert prefabs from
    private float averageRating;

    [Header("InGameSliders")]
    [SerializeField] private Slider StarSlider3;
    [SerializeField] private Slider StarSlider2;
    [SerializeField] private Slider StarSlider1;

    [Header("EndScreenSliders")]
    [SerializeField] private Slider StarSlider3End;
    [SerializeField] private Slider StarSlider2End;
    [SerializeField] private Slider StarSlider1End;

    private float currQuality;

    void Start()
    {
        averageRating = 0f;

        currQuality = 0f;
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
        else if (2f < currQuality && currQuality < 3f){
            StarSlider1.value = 1f;
            StarSlider2.value = 1f;
            StarSlider3.value = currQuality-2;
        }
        else if (currQuality >= 3f){
            StarSlider1.value = 1f;
            StarSlider2.value = 1f;
            StarSlider3.value = 1f;
        }
    }

    public float GetQuality(){
        averageRating = 0f;
        foreach (DessertController dessert in dessertSpawn.desserts)
            {
                averageRating += dessert.GetQuality();
            }
        return (averageRating/(dessertSpawn.desserts.Count-1));
    }

    public void SetEndSliders(){
        // called Update but replaced with the same code because it wasn't working
        currQuality = GetQuality(); // current quality of all the desserts = the quality of your restaurant

        if (currQuality <= 1f)
        {
            StarSlider1.value = currQuality;
            StarSlider2.value = 0f;
            StarSlider3.value = 0f;
        }
        else if (1f < currQuality && currQuality <= 2f)
        {
            StarSlider1.value = 1f;
            StarSlider2.value = currQuality - 1;
            StarSlider3.value = 0f;
        }
        else if (2f < currQuality && currQuality <= 3f)
        {
            StarSlider1.value = 1f;
            StarSlider2.value = 1f;
            StarSlider3.value = currQuality - 2;
        }
        else if (currQuality == 3f)
        {
            StarSlider1.value = 1f;
            StarSlider2.value = 1f;
            StarSlider3.value = 1f;
        }

        restaurantData.SetStars(currQuality);
        StarSlider1End.value = StarSlider1.value;
        StarSlider2End.value = StarSlider2.value;
        StarSlider3End.value = StarSlider3.value;
    }
}
