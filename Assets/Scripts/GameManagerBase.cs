using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class GameManagerBase : MonoBehaviour
{
    public ShopManager ShopManager;
    public TimeManager TimeManager;
    public AudioManager AudioManager;

    public abstract void RecordEnemyKilled(int resourceBounty);

    public abstract void GameOver();

    public void LowerVolumeOnPause()
    {
        if (TimeManager.gameIsPaused)
        {
            AudioManager.ChangeSoundSettings("GameTheme", .198f, 1);
        }
        else
        {
            AudioManager.ResetSoundSettings("GameTheme");
        }
    }
}
