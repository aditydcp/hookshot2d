using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class GameManagerBase : MonoBehaviour
{
    public ShopManager ShopManager;
    public TimeManager TimeManager;

    public abstract void RecordEnemyKilled(int resourceBounty);

    public abstract void GameOver();
}
