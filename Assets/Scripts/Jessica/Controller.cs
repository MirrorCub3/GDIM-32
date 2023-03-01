using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jessica Lam
public class Controller : MonoBehaviour
{

    public void NextScene()
    {
        GameManager.instance.UnloadKitchen();
    }
}
