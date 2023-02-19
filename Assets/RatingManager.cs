using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingManager : MonoBehaviour
{
    public Slider StarSlider3;
    public Slider StarSlider2;
    public Slider StarSlider1;

    void Start()
    {
        StarSlider3.value = 0f;
        StarSlider2.value = 0f;
        StarSlider1.value = 0f;
    }

    void Update()
    {
        
    }
}
