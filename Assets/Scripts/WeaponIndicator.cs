using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIndicator : MonoBehaviour
{
    public PlayerShooting PlayerShooting;
    public Image GrapplingHookImage;
    public Image GunImage;

    private PlayerShooting.PlayerWeapon _previousWeapon;

    // Start is called before the first frame update
    void Start()
    {
        _previousWeapon = PlayerShooting.SelectedWeapon;
        HighlightCurrentWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        // On weapon change:
        if (PlayerShooting.SelectedWeapon != _previousWeapon)
        {
            UnhighlightAllImages();

            HighlightCurrentWeapon();

            _previousWeapon = PlayerShooting.SelectedWeapon;
        }
    }

    private void HighlightCurrentWeapon()
    {
        switch (PlayerShooting.SelectedWeapon)
        {
            case PlayerShooting.PlayerWeapon.Gun:
                GunImage.color = Color.blue;
                return;
            case PlayerShooting.PlayerWeapon.GrapplingHook:
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
