using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEnemyManager : EnemyManagerBase
{
    public List<GameObject> LaserTurrets;

    public List<GameObject> RightMissileTurrets;
    public List<GameObject> LeftMissileTurrets;

    public List<GameObject> RightGunTurrets;
    public List<GameObject> LeftGunTurrets;

    public GameObject BossHealthBar;

    public IEnumerator _attackCoroutine;

    private bool _isActivated = false;

    private void Start()
    {

    }

    public void ActivateBoss()
    {
        if (!_isActivated)
        {
            _isActivated = true;

            BossHealthBar.SetActive(true);

            var healthbar = BossHealthBar.GetComponent<ProgressBar>();
            healthbar.SetMaxProgress(MaxHealth);
            healthbar.SetCurrentProgress(MaxHealth);

            _attackCoroutine = StartAttackPattern();
            StartCoroutine(_attackCoroutine);
        }
    }

    private IEnumerator StartAttackPattern()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            yield return StartSustainedFire(duration: 5f);

            yield return StartMachinegunBarrage(0.8f, duration: 3f);

            yield return new WaitForSeconds(25f);

            yield return StartMissileBarrage(RightMissileTurrets, 2, 6f);

            yield return new WaitForSeconds(1f);

            yield return StartMissileBarrage(LeftMissileTurrets, 2, 6f);

            yield return new WaitForSeconds(3f);

            yield return StartShootingLasers(duration: 5f);

            yield return new WaitForSeconds(5f);

            yield return StartMachinegunBarrage(0.5f, duration: 1f);

            StartCoroutine(StartMissileBarrage(
                CombineLists(RightMissileTurrets, LeftMissileTurrets),
                5,
                6f
            ));
            StartCoroutine(StartShootingLasers(duration: 30f));
            yield return new WaitForSeconds(30f);

            yield return new WaitForSeconds(10f);
        }
    }

    private IEnumerator StartMachinegunBarrage(float interval, float duration)
    {
        var shootMachinegunCoroutine = ShootMachinegun();

        StartCoroutine(shootMachinegunCoroutine);

        yield return new WaitForSeconds(duration);

        StopCoroutine(shootMachinegunCoroutine);

        IEnumerator ShootMachinegun()
        {
            while(true)
            {
                CombineLists(RightGunTurrets, LeftGunTurrets).ForEach(turret =>
                {
                    turret.GetComponent<IEnemyProjectileWeapon>().FireOnce();
                });

                yield return new WaitForSeconds(interval);
            }
        }
    }

    private IEnumerator StartSustainedFire(float duration)
    {
        var gunTurrets = CombineLists(RightGunTurrets, LeftGunTurrets);

        gunTurrets.ForEach(turret =>
        {
            turret.GetComponent<IEnemyWeapon>().StartShooting();
        });

        yield return new WaitForSeconds(duration);

        gunTurrets.ForEach(turret =>
        {
            turret.GetComponent<IEnemyWeapon>().StopShooting();
        });
    }

    private IEnumerator StartMissileBarrage(List<GameObject> turrets, int count, float interval)
    {
        for (int i = 0; i < count; i++)
        {
            turrets.ForEach(turret =>
            {
                turret.GetComponent<IEnemyProjectileWeapon>().FireOnce();
            });

            yield return new WaitForSeconds(interval);
        }
    }

    private IEnumerator StartShootingLasers(float duration)
    {
        LaserTurrets.ForEach(turret =>
        {
            turret.GetComponent<IEnemyWeapon>().StartShooting();
        });

        yield return new WaitForSeconds(duration);

        LaserTurrets.ForEach(turret =>
        {
            turret.GetComponent<IEnemyWeapon>().StopShooting();
        });
    }

    private List<T> CombineLists<T> (List<T> list1, List<T> list2)
    {
        var newList = new List<T>();

        newList.AddRange(list1);
        newList.AddRange(list2);

        return newList;
    }

    public override void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        BossHealthBar.GetComponent<ProgressBar>().SetCurrentProgress(CurrentHealth);

        if (CurrentHealth < 0)
        {
            BossHealthBar.SetActive(false);
            Debug.Log("Boss dead.");

            SceneManager.LoadScene("EpilogueScene");
        }
    }
}
