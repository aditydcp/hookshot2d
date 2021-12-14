using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public class Upgrade
    {
        public int ResourceCost;
        public int CurrentLevel;
        public int MaxLevel;
    }

    public GameObject ShopMenu;

    public int Resources = 0;

    public int RifleDamageIncrease;
    public Upgrade RifleDamageUpgrade = new Upgrade()
    {
        ResourceCost = 100,
        CurrentLevel = 0,
        MaxLevel = 10
    };

    public int ShotgunBulletCountIncrease;
    public Upgrade ShotgunPelletCountUpgrade = new Upgrade()
    {
        ResourceCost = 150,
        CurrentLevel = 0,
        MaxLevel = 6
    };

    public float MachinegunFireIntervalDecrease;
    public Upgrade MachineGunFirerateUpgrade = new Upgrade()
    {
        ResourceCost = 200,
        CurrentLevel = 0,
        MaxLevel = 10
    };

    public void AddResource(int amount)
    {
        Resources += amount;
    }

    public void UpgradeRifleDamage()
    {
        MakePurchase(RifleDamageUpgrade, () =>
        {
            RifleDamageIncrease += 1;
        });
    }

    public void UpgradeShotgunBulletCount()
    {
        MakePurchase(ShotgunPelletCountUpgrade, () =>
        {
            ShotgunBulletCountIncrease += 1;
        });
    }

    public void UpgradeMachinegunFirerate()
    {
        MakePurchase(MachineGunFirerateUpgrade, () =>
        {
            MachinegunFireIntervalDecrease += 0.005f;
        });
    }

    public void OpenShop()
    {
        ShopMenu.SetActive(true);
    }

    public void CloseShop()
    {
        ShopMenu.SetActive(false);
    }

    private void MakePurchase(Upgrade upgrade, Action successfulPurchaseAction)
    {
        if (Resources >= upgrade.ResourceCost && upgrade.CurrentLevel < upgrade.MaxLevel)
        {
            Resources -= upgrade.ResourceCost;

            upgrade.CurrentLevel += 1;

            successfulPurchaseAction.Invoke();
        }
    }
}
