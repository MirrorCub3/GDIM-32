using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KitchenManager : MonoBehaviour
{
    // private string currentDessertRestaurant;
    // private int currentBPM;
    public GameObject BeforeGameCanvas;
    public GameObject InGameCanvas;

    public bool chosen;

    public GameObject DessertsChosen;
    TextMeshProUGUI textmeshpro_dessertschosen;
    private int dessertsChosen;

    public GameObject DessertsLeftDisplay;
    TextMeshProUGUI textmeshpro_dessertsleft;
    private int dessertsLeft;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        chosen = false;
        BeforeGameCanvas.SetActive(true);
        InGameCanvas.SetActive(false);
        textmeshpro_dessertschosen = DessertsChosen.GetComponent<TMPro.TextMeshProUGUI>();
        textmeshpro_dessertsleft = DessertsLeftDisplay.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chosen){
            textmeshpro_dessertsleft.text = dessertsLeft.ToString();
        }
    }

    public void StartGameCycle(){
        Time.timeScale = 1f;
        dessertsChosen = int.Parse(textmeshpro_dessertschosen.text);

        dessertsLeft = dessertsChosen;
        chosen = true;

        BeforeGameCanvas.SetActive(false);
        InGameCanvas.SetActive(true);
    }
}
