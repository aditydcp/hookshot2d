using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressCutscene : MonoBehaviour
{
    public List<GameObject> Cutscenes;
    public string NextSceneName;

    private int _cutsceneIndex = 0;

    public void Next()
    {
        _cutsceneIndex += 1;

        if (_cutsceneIndex < Cutscenes.Count)
        {
            Cutscenes[_cutsceneIndex].SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(NextSceneName);
        }
    }
}
