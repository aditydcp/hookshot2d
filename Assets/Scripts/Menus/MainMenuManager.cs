using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // this function will be called when EndlessButton is clicked
    public void PlayEndlessMode()
    {
        // load scene 1 (which is Sample Scene)
        // this index number can be configured in Build Settings...
        SceneManager.LoadScene(1);
    }

    // call to quit
    public void QuitGame()
    {
        Debug.Log("Quitted!");
        Application.Quit(); // this won't execute in editor mode
    }
}