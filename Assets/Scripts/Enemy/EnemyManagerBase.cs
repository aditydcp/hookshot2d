using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyManagerBase : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth = 100;
    public float Range = 10f;

    public abstract void TakeDamage(int damageAmount);

    public bool PlayerIsInRange()
    {
        var player = GameObject.Find("Player");
        Vector2 playerDistance = player.transform.position - transform.position;

        return playerDistance.magnitude < Range;
    }
}
