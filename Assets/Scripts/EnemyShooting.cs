using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform FiringPoint;
    public GameObject BulletPrefab;

    public float BulletForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 1f, 1f);
    }

    void Shoot()
    {
        var bullet = Instantiate(BulletPrefab, FiringPoint.position, FiringPoint.rotation);

        var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        bulletRigidbody.AddForce(FiringPoint.up * BulletForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
