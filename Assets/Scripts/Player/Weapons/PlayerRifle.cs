using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRifle : ProjectileWeaponBase, IPlayerWeapon
{
    public ShopManager ShopManager;

    protected override void Fire()
    {
        AudioManager.Play("Fire");

        var bullet = Instantiate(BulletPrefab, FiringPoint.position, FiringPoint.rotation);

        bullet.GetComponent<PlayerBullet>().Damage += ShopManager.RifleDamageIncrease;

        var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        bulletRigidbody.AddForce(FiringPoint.up * BulletForce, ForceMode2D.Impulse);
    }
}
