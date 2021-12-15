using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryGameManager : GameManagerBase
{
    public GameObject GameOverOverlay;

    private GameObject _player;
    public BossEnemyManager BossEnemyManager;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.transform.position.y > 325)
        {
            BossEnemyManager.ActivateBoss();
        }

        LowerVolumeOnPause();
    }

    public override void RecordEnemyKilled(int resourceBounty)
    {
        ShopManager.AddResource(resourceBounty);
    }

    public override void GameOver()
    {
        GameOverOverlay.SetActive(true);
        TimeManager.Pause();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
