using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyWeapon
{
    bool IsShooting { get; }
    void StartShooting();
    void StopShooting();
}
