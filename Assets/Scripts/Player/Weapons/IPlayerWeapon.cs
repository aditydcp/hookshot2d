using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerWeapon
{
    bool IsShooting { get; }
    void StartShooting();
    void StopShooting();
}
