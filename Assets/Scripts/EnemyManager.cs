using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth = 100;
    public int regenerationPerSecond = 5;

    public ProgressBar HealthBar;
    public GameObject Turret;

    public GameObject TurretPrefab;

    public IEnumerator RegenerationCoroutine;

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

    private IEnumerator Regenerate()
    {
        while (CurrentHealth < MaxHealth)
        {
            if (CurrentHealth + regenerationPerSecond < MaxHealth)
            {
                CurrentHealth += regenerationPerSecond;
            }
            else
            {
                CurrentHealth = MaxHealth;
            }

            HealthBar.SetCurrentProgress(CurrentHealth);

            yield return new WaitForSeconds(1f);
        }

        Revive();
    }

    private void Revive()
    {
        Turret = Instantiate(TurretPrefab, transform);

        Turret.transform.localPosition = new Vector2(0.01f, -0.87f);
    }

    public void TakeDamage(int damageAmount)
    {
        if (CurrentHealth - damageAmount > 0)
        {
            CurrentHealth -= damageAmount;
        }
        else
        {
            CurrentHealth = 0;
        }

        if (CurrentHealth == 0 && Turret != null)
        {
            var TurretRigidbody = Turret.GetComponent<Rigidbody2D>();
            TurretRigidbody.constraints = RigidbodyConstraints2D.None;

            StartCoroutine(Regenerate());

            Destroy(Turret, 10f);

            Turret = null;
        }

        HealthBar.SetCurrentProgress(CurrentHealth);
    }
}
