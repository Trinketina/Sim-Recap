using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] HealthSystem health;

    public UnityEvent<int,int> OnHeal, OnDamage;

    [ContextMenu("Heal")]
    public void Heal(int amt)
    {
        health.Health += amt;
        OnHeal.Invoke(health.Health, health.MaxHealth);
    }
    [ContextMenu("TakeDamage")]
    public void TakeDamage(int amt)
    {
        health.Health -= amt;
        OnDamage.Invoke(health.Health, health.MaxHealth);

        if (health.Health < 0)
        {
            //do something
        }
    }

    private void Start()
    {
        OnHeal.Invoke(health.Health, health.MaxHealth);
    }
}
