using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Composite Class
public class RatingManager : MonoBehaviour, Quality
{
    public DessertSpawner dessertSpawn; // script to take list of dessert prefabs from
    float averageRating;

    public Slider StarSlider3;
    public Slider StarSlider2;
    public Slider StarSlider1;

    void Start()
    {
        averageRating = 0f;

        StarSlider3.value = 0f;
        StarSlider2.value = 0f;
        StarSlider1.value = 0f;
    }

    public float GetQuality(){
        foreach (Quality dessert in dessertSpawn.desserts)
            {
                averageRating += dessert.GetQuality();
            }
        return (averageRating/(dessertSpawn.desserts.Count));
    }
}
