using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMachinegunLabel : MonoBehaviour
{
    public Text Text;
    public ShopManager ShopManager;

    private void Update()
    {
        Text.text = $"Upgrade Machinegun ({ShopManager.MachineGunFirerateUpgrade.ResourceCost})";
    }
}
