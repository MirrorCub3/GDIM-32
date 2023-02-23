using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Joyce Mai
public class GameManager : MonoBehaviour
{
    public static GameManager instance; // reference to this singleton

    [Header("Scene Management")]
    [SerializeField] private string mainMenuScene = "MainMenu";
    [SerializeField] private string baseScene = "OuterWorld";
    private GameObject outerWorld;

    // book keeping variables for scene management
    private string currScene;
    private bool playing;
    private bool inKitchen;

    [Header("Inventory")] 
    [SerializeField] private InventoryData inventoryData;

    [Header("Loading")] // revist this later
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingBar;

    [HideInInspector] public bool paused { get; private set; }

    void Awake()
    {
        // singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        currScene = SceneManager.GetActiveScene().name;

        if (currScene != mainMenuScene)
            playing = true;

        loadingScreen.SetActive(false);

        Resume();
    }
    
    private void OnDestroy()
    {
        // makes sure the data is reset between plays
        inventoryData.Reset();
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

    public void ToMainMenu() // simple call to menu, for ease of access
    {
        LoadScene(mainMenuScene);
        RevenueManager.instance.Reset();
    }

    public void Restart()
    {
        inventoryData.Reset();
        RevenueManager.instance.Reset();
        // reset all restaurants here too

        SceneManager.LoadScene(currScene);
    }

    public void LoadScene(string nextScene = "OuterWorld")
    {
        Resume(); // makes sure scenes dont load in paused

        if (nextScene == mainMenuScene && inKitchen) // if the current scene is the kitchen make sure to unload it first
            UnloadKitchen();

        currScene = nextScene; 
        
        if (currScene != mainMenuScene)
            playing = true;

        SceneManager.LoadScene(currScene);

        loadingScreen.SetActive(false);
    }

    public void SetOuterWorldToggle(GameObject toggle)
    {
        outerWorld = toggle;
    }

    public void LoadKitchen(string sceneName) // loads the kitchen scene over the overorld scene
    {
        if (!playing || inKitchen)
        {
            Debug.Log("You should not be loading kitchens from the main menu or from other kitchens");
            return;
        }
        StopAllCoroutines();
        StartCoroutine(LoadKitchenAsync(sceneName));
    }
    public void UnloadKitchen() // starts unloading process of the kitchen
    {
        if (!playing || !inKitchen)
        {
            Debug.Log("you cannot unload a null kitchen");
            return;
        }
        StopAllCoroutines();
        StartCoroutine(UnloadIntoBaseAsync(currScene));
    }

    private IEnumerator LoadKitchenAsync(string scene) // used to unload the kitchen scene and reactivate the base world
    {
        inKitchen = true;

        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        while (!operation.isDone)
        {
            float progess = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progess;
            yield return null;
        }
        outerWorld.SetActive(false);
        loadingScreen.SetActive(false);
        currScene = scene;
    }

    private IEnumerator UnloadIntoBaseAsync(string scene) // used to unload the kitchen scene and reactivate the base world
    {
        AsyncOperation operation = SceneManager.UnloadSceneAsync(scene);
        while (!operation.isDone)
        {
            yield return null;
        }
        inKitchen = false;

        if (outerWorld != null) // if the scene being loaded into is the base scene
        {
            currScene = baseScene;
            outerWorld.SetActive(true);
        }
    }

}
