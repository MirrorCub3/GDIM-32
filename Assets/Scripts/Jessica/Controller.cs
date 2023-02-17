using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public Sweets sweet; //temporarily here to just stop the music when leaving

    public void NextScene()
    {
        //SceneManager.LoadScene("OuterWorld");
        GameManager.instance.UnloadKitchen();
    }
}
