using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Naman Khurana

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private JournalTab journalTab;
    private List<AudioSource> audioPlaying = new List<AudioSource>();

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
        journalTab.CloseContent();

        foreach (AudioSource audio in audioPlaying)
        {
            audio.UnPause();
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameManager.instance.Pause();
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        audioPlaying = new List<AudioSource>();

        foreach (AudioSource audio in audios)
        {
            if (audio.isPlaying)
            {
                audioPlaying.Add(audio);
                audio.Pause();
            }
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

