using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRifle : ProjectileWeaponBase, IPlayerWeapon
{
    protected override void Fire()
    {
        var bullet = Instantiate(BulletPrefab, FiringPoint.position, FiringPoint.rotation);

        var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        bulletRigidbody.AddForce(FiringPoint.up * BulletForce, ForceMode2D.Impulse);
    }
}
