using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public TimeManager TimeManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (TimeManager.gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        TimeManager.Resume();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        TimeManager.Pause();
    }

    public void LoadMainMenu()
    {
        TimeManager.Resume();
        SceneManager.LoadScene(0);
    }
}
