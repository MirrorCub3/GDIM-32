using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.instance.paused)
            {
                Resume();
            } else
            {
                Pause();

            }
        }
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameManager.instance.Pause();

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameManager.instance.Resume();



    }

    public void LoadMenu()
    {
        GameManager.instance.ToMainMenu();

    }

    public void QuitGame()
    {
        Application.Quit();
    }


}

