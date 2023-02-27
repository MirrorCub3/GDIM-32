using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Joyce Mai
public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] string winMessage = "You Win!";
    [SerializeField] string loseMessage = "Game Over";

    private void Awake()
    {
        if (GameManager.instance.state == GameManager.GameState.WIN)
            message.text = winMessage;
        else
            message.text = loseMessage;
    }

    public void ToMain()
    {
        GameManager.instance.ToMainMenu();
    }
}
