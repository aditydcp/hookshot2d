using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform FiringPoint;
    public Rigidbody2D PlayerRigidbody;
    public GameObject BulletPrefab;
    public GameObject GrapplingHookPrefab;

    public float BulletForce = 10f;
    public float GrapplingHookForce = 5f;

    public PlayerHook _currentGrapplingHook = null;
    public PlayerWeapon SelectedWeapon = PlayerWeapon.Gun;

    public TimeManager TimeManager;

    public enum PlayerWeapon
    {
        Gun,
        GrapplingHook
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeapons();

        if (Input.GetButtonDown("Fire1"))
        {
            switch (SelectedWeapon)
            {
                case PlayerWeapon.Gun:
                    Shoot();
                    return;
                case PlayerWeapon.GrapplingHook:
                    if (_currentGrapplingHook == null)
                    {
                        ShootGrapplingHook();
                    }
                    else
                    {
                        DetachGrapplingHook();
                    }
                    return;
            }
        }
    }

    private void ChangeWeapons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_currentGrapplingHook != null)
            {
                DetachGrapplingHook();
            }

            SelectedWeapon = PlayerWeapon.Gun;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectedWeapon = PlayerWeapon.GrapplingHook;
        }

        if (SelectedWeapon == PlayerWeapon.Gun)
        {
            TimeManager.EnableSlowmotion();
        }
        else
        {
            TimeManager.DisableSlowmotion();
        }
    }

    void ShootGrapplingHook()
    {
        var grapplingHook = Instantiate(GrapplingHookPrefab, FiringPoint.position, FiringPoint.rotation);

        grapplingHook.GetComponent<Rigidbody2D>().AddForce(FiringPoint.up * GrapplingHookForce, ForceMode2D.Impulse);

        _currentGrapplingHook = grapplingHook.GetComponent<PlayerHook>();

        _currentGrapplingHook.Attach(PlayerRigidbody, FiringPoint);
    }

    void DetachGrapplingHook()
    {
        _currentGrapplingHook.Detach();

        _currentGrapplingHook = null;
    }

    void Shoot()
    {
        var bullet = Instantiate(BulletPrefab, FiringPoint.position, FiringPoint.rotation);

        var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        bulletRigidbody.AddForce(FiringPoint.up * BulletForce, ForceMode2D.Impulse);
    }
}
