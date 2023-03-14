using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Naman Khurana
public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private string singleScene = "OuterWorldSingle";
    [SerializeField] private string multiScene = "OuterWorld";
    public void PlayGameSingle()
    {
        GameManager.instance.SetPlayMode(GameManager.PlayMode.SINGLE);
        GameManager.instance.LoadScene(singleScene);
    }

    public void PlayGameMulti()
    {
        GameManager.instance.SetPlayMode(GameManager.PlayMode.MULTI);
        GameManager.instance.LoadScene(multiScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}