using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Joyce Mai
public class EndScreen : MonoBehaviour
{
    public void ToMain()
    {
        GameManager.instance.ToMainMenu();
    }
}
