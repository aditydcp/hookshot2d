using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponAimer : MonoBehaviour
{
    public Rigidbody2D Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var player = GameObject.Find("Player");
        Vector2 playerPosition = player.transform.position;

        var lookDirection = playerPosition - Rigidbody.position;

        var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

        Rigidbody.rotation = angle;
    }
}
