using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int Resources = 0;

    public void AddResource(int amount)
    {
        Resources += amount;
    }


}
