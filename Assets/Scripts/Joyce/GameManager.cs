using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Destroy(this);
            return;
        }

        currScene = SceneManager.GetActiveScene().name;

        if (currScene != mainMenuScene)
            playing = true;

        if (currScene == baseScene)
            FindOuterWorldToggle();

        Resume();
    }
    private void OnDestroy()
    {
        // makes sure the data is reset between plays
        inventoryData.Reset();
    }

    //used to test unloading and loading kitchens
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        LoadKitchen("CookieKitchen");
    //    }
    //    if (Input.GetKeyDown(KeyCode.U))
    //    {
    //        UnloadKitchen();
    //    }
    //}

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
    }

    public void Restart()
    {
        inventoryData.Reset();
        // reset all restaurants here too

        SceneManager.LoadScene(currScene);
    }

    public void LoadScene(string nextScene = "OuterWorld")
    {
        Resume(); // makes sure scenes dont load in paused

        if (nextScene == mainMenuScene && inKitchen) // if the current scene is the kitchen make sure to unload it first
            UnloadKitchen();

        currScene = nextScene;
        SceneManager.LoadScene(currScene);

        if (currScene == baseScene)
            FindOuterWorldToggle();
    }

    public void FindOuterWorldToggle() // called when loading into the base scene
    {
        outerWorld = GameObject.FindGameObjectWithTag("OuterWorld");
    }

    public void LoadKitchen(string sceneName) // loads the kitchen scene over the overorld scene
    {
        if (!playing)
        {
            Debug.Log("You should not be loading kitchens from the main menu");
            return;
        }

        inKitchen = true;
        currScene = sceneName;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        outerWorld.SetActive(false);
    }
    public void UnloadKitchen() // starts unloading process of the kitchen
    {
        if (!playing || !inKitchen)
        {
            Debug.Log("you cannot unload a null kitchen");
            return;
        }

        StartCoroutine(UnloadIntoBaseAsync(currScene));
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
