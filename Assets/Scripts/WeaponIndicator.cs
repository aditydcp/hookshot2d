using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIndicator : MonoBehaviour
{
    public PlayerShooting PlayerShooting;
    public Image GrapplingHookImage;
    public Image GunImage;

    private PlayerShooting.PlayerGunType _previousWeapon;

    // Start is called before the first frame update
    void Start()
    {
        _previousWeapon = PlayerShooting.SelectedGun;
        HighlightCurrentWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        // On weapon change:
        if (PlayerShooting.SelectedGun != _previousWeapon)
        {
            UnhighlightAllImages();

            HighlightCurrentWeapon();

            _previousWeapon = PlayerShooting.SelectedGun;
        }
    }

    private void HighlightCurrentWeapon()
    {
        switch (PlayerShooting.SelectedGun)
        {
            case PlayerShooting.PlayerGunType.Weapon:
                GunImage.color = Color.blue;
                return;
            case PlayerShooting.PlayerGunType.GrapplingHook:
                GrapplingHookImage.color = Color.blue;
                return;
        }
    }

    private void UnhighlightAllImages()
    {
        GrapplingHookImage.color = Color.white;
        GunImage.color = Color.white;
    }
}
