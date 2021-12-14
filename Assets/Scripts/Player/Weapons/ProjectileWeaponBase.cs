using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileWeaponBase : MonoBehaviour, IPlayerWeapon
{
    public float InitialCooldown = 0.5f;
    public float FiringInterval = 1f;
    public float BulletForce = 10f;

    public Transform FiringPoint;
    public GameObject BulletPrefab;

    public bool IsShooting { get; private set; }

    private IEnumerator _shootingCoroutine = null;

    public void StartShooting()
    {
        if (!IsShooting)
        {
            IsShooting = true;

            _shootingCoroutine = Shoot();

            StartCoroutine(_shootingCoroutine);
        }
    }

    public void StopShooting()
    {
        if (IsShooting)
        {
            IsShooting = false;

            StopCoroutine(_shootingCoroutine);

            _shootingCoroutine = null;
        }
    }

    protected abstract void Fire();

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(InitialCooldown);

        while (true)
        {
            Fire();

            yield return new WaitForSeconds(FiringInterval);
        }
    }
}
