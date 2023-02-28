using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Naman Khurana

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
        GameManager.instance.Resume();
        AudioSource[] audios = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in audios)
        {
            audio.Play();
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameManager.instance.Pause();
        AudioSource[] audios = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in audios)
        {
            audio.Pause();
        }
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

