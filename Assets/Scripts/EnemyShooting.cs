using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform FiringPoint;
    public GameObject BulletPrefab;
    
    public float MinInitialShotDelay = 2f;
    public float InitialShotDelayVariation = 2f;

    public float MinShootingInterval = 5f;
    public float ShootingIntervalVariation = 2f;

    public float BulletForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", MinInitialShotDelay + Random.Range(0f, InitialShotDelayVariation), MinShootingInterval + Random.Range(0f, ShootingIntervalVariation));
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
