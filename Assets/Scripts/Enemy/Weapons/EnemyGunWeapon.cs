using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunWeapon : MonoBehaviour, IEnemyProjectileWeapon
{
    public bool IsShooting { get; private set; }

    public Transform FiringPoint;
    public GameObject BulletPrefab;

    public float MinInitialShotDelay = 2f;
    public float InitialShotDelayVariation = 2f;

    public float MinShootingInterval = 5f;
    public float ShootingIntervalVariation = 2f;

    public float BulletForce = 10f;

    private IEnumerator _shootingCoroutine = null;

    public void StartShooting()
    {
        if (_shootingCoroutine == null)
        {
            _shootingCoroutine = Shoot();

            StartCoroutine(_shootingCoroutine);
        }
    }

    public void StopShooting()
    {
        if (_shootingCoroutine != null)
        {
            StopCoroutine(_shootingCoroutine);

            _shootingCoroutine = null;
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(MinInitialShotDelay + Random.Range(0f, InitialShotDelayVariation));

        while(true)
        {
            FireOnce();

            yield return new WaitForSeconds(MinShootingInterval + Random.Range(0f, ShootingIntervalVariation));
        }
    }

    public void FireOnce()
    {
        var bullet = Instantiate(BulletPrefab, FiringPoint.position, FiringPoint.rotation);

        var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        bulletRigidbody.AddForce(FiringPoint.up * BulletForce, ForceMode2D.Impulse);

        FindObjectOfType<AudioManager>().Play("EnemyFire");
    }
}
