using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Naman Khurana
public class MainMenuScript : MonoBehaviour
{

    public void PlayGame()
    {
        GameManager.instance.LoadScene("OuterWorld");
    }


    public void Quit()
    {
        Application.Quit();
    }
}