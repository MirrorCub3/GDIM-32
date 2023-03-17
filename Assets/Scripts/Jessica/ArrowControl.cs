using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    TextMeshProUGUI textmeshpro_maxpopup;

    [SerializeField] Sweets sweetType;

    int dessertMax;

    // Start is called before the first frame update
    void Start()
    {
        textmeshpro_number = numberToIncrease.GetComponent<TMPro.TextMeshProUGUI>();
        textmeshpro_max_number = maxAvailable.GetComponent<TMPro.TextMeshProUGUI>();
        textmeshpro_maxpopup = max100popup.GetComponent<TMPro.TextMeshProUGUI>();

        maxCount = int.Parse(textmeshpro_max_number.text);

        Debug.Log(sweetType.sweetName);

        if (sweetType)
        {
            if (sweetType.sweetName == "Ice Cream")
            {
                SetMax(50);
                dessertMax = 50;
            }
            else
            {
                SetMax();
                dessertMax = 100;
            }
        }
        else
        {
            SetMax();
            dessertMax = 100;
        }   
    }

    void SetMax(int customMax = 100)
    {
        Debug.Log("The custom max is: " + customMax);
        textmeshpro_maxpopup.text = "Max " + customMax;
        if (maxCount <= customMax)
        {
            currentCount = maxCount;
            max100popup.SetActive(false);
        }
        else
        {
            currentCount = customMax;
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
        if (currentCount < maxCount && currentCount < dessertMax)
        {
            currentCount++;
            FindObjectOfType<AudioManager>().Play("Arrow Kitchen");
            max100popup.SetActive(false);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Arrow Deny");
            if (currentCount == dessertMax)
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
