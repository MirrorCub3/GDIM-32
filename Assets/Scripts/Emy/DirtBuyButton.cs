using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Emily Chavez
public class DirtBuyButton : MonoBehaviour 
{
    [Header("Item Info")]
    [SerializeField] private List<PlantingPatch> dirts;
    [SerializeField] private int cost;
    private int currIndex = 0;

    [Header("Visuals")]
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button myButton;

    private void Start()
    {
        currIndex = 0;
        costText.text = cost.ToString();
        myButton.interactable = false;

    }

    private void CheckCanUse(int currCoins)
    {
        if (currIndex >= dirts.Count)
        {
            myButton.interactable = false;
            return;
        }
        myButton.interactable = (currCoins >= cost) && currIndex < dirts.Count;
    }

    private void Update()
    {
        CheckCanUse(RevenueManager.instance.coins);
    }

    public void Purchase() //subtract cost from rev 
    {
        if (currIndex >= dirts.Count)
            return;
        dirts[currIndex].Unlock();
        currIndex++;
        RevenueManager.instance.ChangeCoins(-cost);
    }
}
