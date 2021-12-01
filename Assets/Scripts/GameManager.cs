using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int SecondsSurvived = 0;

    public GameObject GameOverOverlay;
    public Text GameScoreText;

    private IEnumerator ScoreCounterCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        ScoreCounterCoroutine = CountScore();
        StartCoroutine(ScoreCounterCoroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CountScore()
    {
        while (true)
        {
            SecondsSurvived += 1;

            yield return new WaitForSeconds(1f);
        }
    }

    public void GameOver()
    {
        StopCoroutine(ScoreCounterCoroutine);
        GameOverOverlay.SetActive(true);
        GameScoreText.text = $"Congratulations, you've managed to survive for {SecondsSurvived} seconds!";
    }
}
