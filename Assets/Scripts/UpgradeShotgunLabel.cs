using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShotgunLabel : MonoBehaviour
{
    public Text Text;
    public ShopManager ShopManager;

    private void Update()
    {
        Text.text = $"Upgrade Shotgun ({ShopManager.ShotgunPelletCountUpgrade.ResourceCost})";
    }
}
