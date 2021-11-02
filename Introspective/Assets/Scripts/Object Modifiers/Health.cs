using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    public float healthMax;
   [HideInInspector] public float healthCurrent;

    [Header ("Requirements")]
    [Space]
    public ProgressBar hpMeter;

    [Header("Events")]
    [Space]
    public UnityEvent OnHurt;
    public UnityEvent OnHeal;
    public UnityEvent OnDeath;


    private void Start()
    {
        healthCurrent = healthMax;
        hpMeter.progress_max = healthMax;
        hpMeter.progress_current = healthCurrent;

        UpdateHealth();
    }


    private void UpdateHealth()
    {
        hpMeter.UpdateValue(healthCurrent);
    }

    public void TakeDamage(float damage, float positionX)
    {
        healthCurrent = Mathf.Clamp(healthCurrent - damage, 0, healthMax);
        print("Hollow Took Damage!");
        print("New Health " + healthCurrent.ToString());

        //Use Position to knockback the player

        OnHurt.Invoke();

        if (healthCurrent <= 0)
        {
            OnDeath.Invoke();
        }

        UpdateHealth();
    }

    public void Heal(int healAmount)
    {
        healthCurrent = Mathf.Clamp(healthCurrent - healAmount, 0, healthMax);

        OnHeal.Invoke();
        UpdateHealth();
    }

}
