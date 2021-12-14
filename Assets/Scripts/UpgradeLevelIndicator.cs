using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeLevelIndicator : MonoBehaviour
{
    public Text Text;

    public ShopManager ShopManager;

    private void Update()
    {
        Text.text = "Upgrade Levels:\n\n" +
            $"Rifle Damage ({ShopManager.RifleDamageUpgrade.CurrentLevel}/{ShopManager.RifleDamageUpgrade.MaxLevel})\n" +
            $"Shotgun Pellets ({ShopManager.ShotgunPelletCountUpgrade.CurrentLevel}/{ShopManager.ShotgunPelletCountUpgrade.MaxLevel})\n" +
            $"Machinegun Fire Rate ({ShopManager.MachineGunFirerateUpgrade.CurrentLevel}/{ShopManager.MachineGunFirerateUpgrade.MaxLevel})";
    }
}
