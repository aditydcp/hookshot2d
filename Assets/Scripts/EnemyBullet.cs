using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
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
        var player = collision.gameObject.GetComponent<PlayerManager>();

        if (player != null)
        {
            player.TakeDamage(10);

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
