using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotgun : ProjectileWeaponBase
{
    public float BulletSpreadDegrees = 15f;
    public int BulletCount = 2;

    protected override void Fire()
    {
        for (int i = 0; i < BulletCount; i++)
        {
            var bullet = Instantiate(BulletPrefab, FiringPoint.position, FiringPoint.rotation);

            var angleBetweenBullets = BulletSpreadDegrees / (BulletCount - 1);

            bullet.transform.Rotate(bullet.transform.forward, -BulletSpreadDegrees / 2 + i * angleBetweenBullets);

            var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

            bulletRigidbody.AddForce(bullet.transform.up * BulletForce, ForceMode2D.Impulse);
        }
    }
}
