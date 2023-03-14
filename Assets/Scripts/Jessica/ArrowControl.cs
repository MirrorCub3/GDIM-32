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
    [SerializeField] private GameObject max100popup;
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
        if (maxCount <= 100)
        {
            currentCount = maxCount;
            max100popup.SetActive(false);
        }
        else
        {
            currentCount = 100;
            max100popup.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        textmeshpro_number.text = currentCount.ToString();
    }

    public void incrementByOne()
    {
        if (currentCount < maxCount && currentCount < 100)
        {
            currentCount++;
            FindObjectOfType<AudioManager>().Play("Arrow Kitchen");
            max100popup.SetActive(false);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Arrow Deny");
            if (currentCount == 100)
            {
                max100popup.SetActive(true);
            }
        }
    }

    public void decrementByOne()
    {
        if (currentCount > 1)
        {
            currentCount = currentCount - 1;
            FindObjectOfType<AudioManager>().Play("Arrow Kitchen");
            max100popup.SetActive(false);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Arrow Deny");
            max100popup.SetActive(false);
        }
    }
}
