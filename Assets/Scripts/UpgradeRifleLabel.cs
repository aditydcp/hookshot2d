using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeRifleLabel : MonoBehaviour
{
    public Text Text;
    public ShopManager ShopManager;

    private void Update()
    {
        Text.text = $"Upgrade Rifle ({ShopManager.RifleDamageUpgrade.ResourceCost})";
    }
}
