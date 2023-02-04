using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Joyce Mai
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Inventory")] 
    [SerializeField] private InventoryData inventoryData;
    
    [Header("Pause Menu")]
    [HideInInspector] public bool paused;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this);
            return;
        }

        //currLevel = SceneManager.GetActiveScene().name;
        //currLevelIndex = levels.IndexOf(currLevel);

        Time.timeScale = 1;
        paused = false;
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1;
    }

    private void OnApplicationQuit()
    {
        inventoryData.ClearInventory();
    }
    //public void ToMainMenu()
    //{
    //    LoadScene("MainMenu");
    //}

    //public void ReloadScene()
    //{
    //    SceneManager.LoadScene(currLevel);
    //}

    //public void LoadScene(string nextScene = "")
    //{
    //    if (nextScene == "") // default to next level up
    //    {
    //        currLevelIndex += 1;
    //        currLevel = levels[currLevelIndex];
    //    }
    //    else // otherwise use the name specified
    //    {
    //        currLevel = nextScene;
    //        currLevelIndex = levels.IndexOf(currLevel);
    //    }

    //    SceneManager.LoadScene(currLevel);
    //}

}
