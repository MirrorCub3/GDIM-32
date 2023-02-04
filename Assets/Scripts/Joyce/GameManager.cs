using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Joyce Mai
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Scene Management")]
    [SerializeField] private string mainMenuScene = "MainMenu";
    [SerializeField] private string baseScene = "OuterWorld";
    private string currScene;
    private bool playing;
    private bool inKitchen;
    private GameObject outerWorld;

    [Header("Inventory")] 
    [SerializeField] private InventoryData inventoryData;

    [HideInInspector] public bool paused { get; private set; }

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

        currScene = SceneManager.GetActiveScene().name;

        if (currScene != mainMenuScene)
            playing = true;

        if (currScene == baseScene)
            FindOuterWorldToggle();

        Resume();
    }

    // used to test unloading and loading kitchens
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

    public void ToMainMenu()
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
        if (nextScene == mainMenuScene && inKitchen)
            UnloadKitchen();

        currScene = nextScene;
        SceneManager.LoadScene(currScene);

        if (currScene == baseScene)
            FindOuterWorldToggle();
    }

    public void FindOuterWorldToggle()
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
    public void UnloadKitchen()
    {
        if (!playing || !inKitchen)
        {
            Debug.Log("you cannot unload a null kitchen");
            return;
        }

        StartCoroutine(UnloadAsync(currScene));
    }

    private IEnumerator UnloadAsync(string scene)
    {
        AsyncOperation operation = SceneManager.UnloadSceneAsync(scene);
        while (!operation.isDone)
        {
            yield return null;
        }
        inKitchen = false;
        currScene = baseScene;
        outerWorld.SetActive(true);
    }

}
