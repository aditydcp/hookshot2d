using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public Transform FiringPoint;
    public Rigidbody2D PlayerRigidbody;
    public GameObject GrapplingHookPrefab;

    public float GrapplingHookForce = 5f;

    private PlayerHook _currentGrapplingHook = null;
    public PlayerGunType SelectedGun = PlayerGunType.Weapon;

    public TimeManager TimeManager;

    public AudioManager AudioManager;

    public PlayerRifle Rifle;
    public PlayerShotgun Shotgun;
    public PlayerMachinegun Machinegun;

    public Text PlayerWeaponIndicator;

    private List<IPlayerWeapon> _weaponCycleList;
    private IPlayerWeapon _selectedWeapon = null;
    private int _selectedWeaponIndex = 0;

    public enum PlayerGunType
    {
        Weapon,
        GrapplingHook
    }

    // Start is called before the first frame update
    void Start()
    {
        _weaponCycleList = new List<IPlayerWeapon>() { Rifle, Shotgun, Machinegun };
        _selectedWeapon = _weaponCycleList[_selectedWeaponIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (!TimeManager.gameIsPaused) // reads inputs only when the game is not paused
        {
            ChangeGuns();
            ChangeWeapons();

            if (Input.GetButton("Fire1") && SelectedGun == PlayerGunType.Weapon)
            {
                _selectedWeapon.StartShooting();
            }
            if (Input.GetButtonDown("Fire1") && SelectedGun == PlayerGunType.Weapon)
            {
                AudioManager.Play("Fire");
            }

            if (Input.GetButtonDown("Fire1") && SelectedGun == PlayerGunType.GrapplingHook)
            {
                if (_currentGrapplingHook == null)
                {
                    ShootGrapplingHook();
                    AudioManager.Play("GrapplingHook");
                }
                else
                {
                    DetachGrapplingHook();
                }
            }

            if (Input.GetButtonUp("Fire1") && SelectedGun == PlayerGunType.Weapon)
            {
                _selectedWeapon.StopShooting();
            }
        }
    }

    private void ChangeGuns()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_currentGrapplingHook != null)
            {
                DetachGrapplingHook();
            }

            SelectedGun = PlayerGunType.Weapon;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectedGun = PlayerGunType.GrapplingHook;
        }

        if (SelectedGun == PlayerGunType.Weapon)
        {
            TimeManager.EnableSlowmotion();
        }
        else
        {
            _selectedWeapon.StopShooting();
            TimeManager.DisableSlowmotion();
        }
    }

    private void ChangeWeapons()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _selectedWeapon.StopShooting();

            _selectedWeaponIndex++;
            if (_selectedWeaponIndex > _weaponCycleList.Count - 1)
            {
                _selectedWeaponIndex = 0;
            }

            switch(_selectedWeaponIndex)
            {
                case 0:
                    PlayerWeaponIndicator.text = "Rifle";
                    break;
                case 1:
                    PlayerWeaponIndicator.text = "Shotgun";
                    break;
                case 2:
                    PlayerWeaponIndicator.text = "Machinegun";
                    break;
                default:
                    PlayerWeaponIndicator.text = "Unidentified";
                    break;
            }

            _selectedWeapon = _weaponCycleList[_selectedWeaponIndex];
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
}
