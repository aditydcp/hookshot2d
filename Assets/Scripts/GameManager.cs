using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ShopManager ShopManager;
    public TimeManager TimeManager;

    public int SecondsSurvived = 0;

    public GameObject GameOverOverlay;
    public Text GameScoreText;

    private IEnumerator ScoreCounterCoroutine;

    private GameObject _player;
    public BossEnemyManager BossEnemyManager;

    // Start is called before the first frame update
    void Start()
    {
        ScoreCounterCoroutine = CountScore();
        StartCoroutine(ScoreCounterCoroutine);

        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.transform.position.y > 325)
        {
            BossEnemyManager.ActivateBoss();
        }   
    }

    IEnumerator CountScore()
    {
        while (true)
        {
            SecondsSurvived += 1;

            yield return new WaitForSeconds(1f);
        }
    }

    public void RecordEnemyKilled(int resourceBounty)
    {
        ShopManager.AddResource(resourceBounty);
    }

    public void GameOver()
    {
        StopCoroutine(ScoreCounterCoroutine);
        GameOverOverlay.SetActive(true);
        TimeManager.Pause();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
