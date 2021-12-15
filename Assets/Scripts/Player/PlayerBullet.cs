using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int Damage = 10;
    public int SelfDestructDelaySeconds = 10;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, SelfDestructDelaySeconds);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<EnemyManagerBase>();

        if (enemy != null)
        {
            enemy.TakeDamage(10);

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
