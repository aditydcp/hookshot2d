using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth = 100;
    public int RegenerationPerSecond = 3;
    public ProgressBar HealthBar;

    public GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.SetMaxProgress(MaxHealth);
        HealthBar.SetCurrentProgress(CurrentHealth);

        StartCoroutine(Regenerate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Regenerate()
    {
        while (true)
        {
            CurrentHealth = Mathf.Min(CurrentHealth + RegenerationPerSecond, MaxHealth);

            HealthBar.SetCurrentProgress(CurrentHealth);

            yield return new WaitForSeconds(1f);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth < 0)
        {
            GameManager.GameOver();
        }

        HealthBar.SetCurrentProgress(CurrentHealth);
    }
}
