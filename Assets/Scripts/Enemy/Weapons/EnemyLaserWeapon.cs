using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserWeapon : MonoBehaviour, IEnemyWeapon
{
    public bool IsShooting { get; private set; }

    public Transform FiringPoint;
    public LineRenderer LineRenderer;
    public float LaserDistance;
    public int Damage = 1;
    public float DamageInterval = 0.5f;

    private IEnumerator _damageDealingCoroutine;

    public void StartShooting()
    {
        if (!IsShooting)
        {
            IsShooting = true;

            _damageDealingCoroutine = DealDamage();

            StartCoroutine(_damageDealingCoroutine);

            FindObjectOfType<AudioManager>().Play("EnemyLaserFire");
        }
    }

    public void StopShooting()
    {
        if (IsShooting)
        {
            IsShooting = false;

            StopCoroutine(_damageDealingCoroutine);

            _damageDealingCoroutine = null;

            FindObjectOfType<AudioManager>().Stop("EnemyLaserFire");
        }
    }

    private void Update()
    {
        if (IsShooting)
        {
            LineRenderer.enabled = true;
            DrawLaser();
        }
        else
        {
            LineRenderer.enabled = false;
        }
    }

    private void DrawLaser()
    {
        var raycastHit = Physics2D.Raycast(FiringPoint.position, FiringPoint.up);

        if (raycastHit)
        {
            LineRenderer.SetPositions(new Vector3[] { FiringPoint.position, raycastHit.point });
        }
    }

    IEnumerator DealDamage()
    {
        while(true)
        {
            var raycastHit = Physics2D.Raycast(FiringPoint.position, FiringPoint.up);

            if (raycastHit)
            {
                var player = raycastHit.collider.GetComponent<PlayerManager>();

                if (player)
                {
                    player.TakeDamage(Damage);
                }
            }

            yield return new WaitForSeconds(DamageInterval);
        }
    }
}
