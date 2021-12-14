using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotgun : ProjectileWeaponBase
{
    public ShopManager ShopManager;

    public float BulletSpreadDegrees = 15f;
    public int BulletCount = 3;

    protected override void Fire()
    {
        var bulletCount = BulletCount + ShopManager.ShotgunBulletCountIncrease;

        for (int i = 0; i < bulletCount; i++)
        {
            var bullet = Instantiate(BulletPrefab, FiringPoint.position, FiringPoint.rotation);

            var angleBetweenBullets = BulletSpreadDegrees / (bulletCount - 1);

            bullet.transform.Rotate(bullet.transform.forward, -BulletSpreadDegrees / 2 + i * angleBetweenBullets);

            var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

            bulletRigidbody.AddForce(bullet.transform.up * BulletForce, ForceMode2D.Impulse);
        }
    }
}
