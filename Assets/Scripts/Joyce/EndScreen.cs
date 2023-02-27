using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Joyce Mai
public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    private void Awake()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        if (GameManager.instance.state == GameManager.GameState.WIN)
            winScreen.SetActive(true);
        else
            loseScreen.SetActive(true);
    }

    public void ToMain()
    {
        GameManager.instance.ToMainMenu();
    }
}
