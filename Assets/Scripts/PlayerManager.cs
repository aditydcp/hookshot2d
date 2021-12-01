using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth = 100;
    public ProgressBar HealthBar;

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

        HealthBar.SetCurrentProgress(CurrentHealth);
    }
}
