using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesIndicator : MonoBehaviour
{
    public Text Text;
    public ShopManager ShopManager;

    private void Update()
    {
        Text.text = $"Resources: {ShopManager.Resources}";
    }
}