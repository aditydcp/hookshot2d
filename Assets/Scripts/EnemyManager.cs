using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth = 100;

    public ProgressBar HealthBar;
    public GameObject Turret;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.SetMaxProgress(MaxHealth);
        HealthBar.SetCurrentProgress(CurrentHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth < 0 && Turret != null)
        {
            var TurretRigidbody = Turret.GetComponent<Rigidbody2D>();
            TurretRigidbody.constraints = RigidbodyConstraints2D.None;

            Destroy(Turret, 10f);
        }

        HealthBar.SetCurrentProgress(CurrentHealth);
    }
}
