using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndlessGameManager : GameManagerBase
{
    public List<GameObject> StageObjectPrefabs;
    public List<float> SpawnChances;

    public int Score = 0;

    public GameObject GameOverOverlay;
    public Text GameScoreText;

    private IEnumerator _survivalScoreCoroutine;

    private void Start()
    {
        StartCoroutine(StartEndlessGameplayLoop());

        _survivalScoreCoroutine = CountSurvivalScore();

        StartCoroutine(_survivalScoreCoroutine);
    }

    IEnumerator CountSurvivalScore()
    {
        while (true)
        {
            Score += 1;

            yield return new WaitForSeconds(1f);
        }
    }

    public override void GameOver()
    {
        GameOverOverlay.SetActive(true);

        StopCoroutine(_survivalScoreCoroutine);

        GameScoreText.text = $"Your score: {Score}";

        TimeManager.Pause();
    }

    public override void RecordEnemyKilled(int resourceBounty)
    {
        ShopManager.AddResource(resourceBounty);
        
        Score += 1;
    }

    public List<GameObject> SpawnObjectsRandomly(int count, float radius)
    {
        var instantiatedObjects = new List<GameObject>();

        var spawnRanges = new List<(float, float)>();

        StageObjectPrefabs.ForEach(prefab =>
        {
            var currentIndex = spawnRanges.Count;

            var min = currentIndex == 0 ? 0 : spawnRanges[currentIndex - 1].Item2;
            var max = min + SpawnChances[currentIndex];

            spawnRanges.Add((min, max));
        });

        var chanceSum = 0f;
        SpawnChances.ForEach(chance => chanceSum += chance);

        for (int i = 0; i < count; i++)
        {
            var spawnPoint = Random.insideUnitCircle * radius;

            // Draw spawned object "lottery".
            var lotteryNumber = Random.Range(0f, chanceSum);

            var prefabIndex = spawnRanges.FindIndex(range => lotteryNumber >= range.Item1 && lotteryNumber <= range.Item2);
            
            instantiatedObjects.Add(Instantiate(StageObjectPrefabs[prefabIndex], spawnPoint, Quaternion.identity));
        }

        return instantiatedObjects;
    }

    public IEnumerator StartEndlessGameplayLoop()
    {
        while (true)
        {
            var enemies = SpawnObjectsRandomly(75, 50f);

            yield return new WaitForSeconds(10f);

            enemies.ForEach(enemy =>
            {
                Destroy(enemy);
            });
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("EndlessMode");
    }
}
