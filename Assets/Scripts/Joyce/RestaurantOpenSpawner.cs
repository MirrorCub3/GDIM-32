using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Joyce Mai
public class RestaurantOpenSpawner : MonoBehaviour
{
    [Header("Spawn List")]
    [SerializeField] private List<GameObject> spawns = new List<GameObject>();

    [Header("UI")]
    [SerializeField] private GameObject display;

    private void Awake()
    {
        foreach (GameObject s in spawns)
            s.SetActive(false);
        ClosePopup();
    }
    public void Spawn()
    {
        foreach (GameObject s in spawns)
            s.SetActive(true);
        display.SetActive(true);
    }

    public void ClosePopup()
    {
        display.SetActive(false);
    }
}
