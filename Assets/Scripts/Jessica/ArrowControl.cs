using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Jessica Lam
public class ArrowControl : MonoBehaviour
{
    // UI text
    [SerializeField] private GameObject numberToIncrease;
    [SerializeField] private GameObject maxAvailable;
    // private variable
    private int maxCount;
    private int currentCount;

    TextMeshProUGUI textmeshpro_number;
    TextMeshProUGUI textmeshpro_max_number;

    // Start is called before the first frame update
    void Start()
    {
        textmeshpro_number = numberToIncrease.GetComponent<TMPro.TextMeshProUGUI>();
        textmeshpro_max_number = maxAvailable.GetComponent<TMPro.TextMeshProUGUI>();
        maxCount = int.Parse(textmeshpro_max_number.text);
        currentCount = maxCount;
    }

    // Update is called once per frame
    void Update()
    {
        textmeshpro_number.text = currentCount.ToString();
    }

    public void incrementByOne(){
        if (currentCount < maxCount){
            currentCount++;
            FindObjectOfType<AudioManager>().Play("Arrow Kitchen");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Arrow Deny");
        }
    }

    public void decrementByOne(){
        if (currentCount > 1){
            currentCount = currentCount-1;
            FindObjectOfType<AudioManager>().Play("Arrow Kitchen");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Arrow Deny");
        }
    }
}
