using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMachinegun : ProjectileWeaponBase
{
    public ShopManager ShopManager;

    public float BulletSpreadDegrees = 10f;
    public float MaxFiringIntervalVariation = 0.01f;

    public float _initialFiringInterval;

    private void Start()
    {
        _initialFiringInterval = FiringInterval;
    }

    protected override void Fire()
    {
        FiringInterval =
            Random.Range(1f, -1f) * MaxFiringIntervalVariation +
            _initialFiringInterval -
            ShopManager.MachinegunFireIntervalDecrease;

        var bullet = Instantiate(BulletPrefab, FiringPoint.position, FiringPoint.rotation);

        bullet.transform.Rotate(bullet.transform.forward, Random.Range(-BulletSpreadDegrees, BulletSpreadDegrees));

        var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        bulletRigidbody.AddForce(bullet.transform.up * BulletForce, ForceMode2D.Impulse);
    }
}
