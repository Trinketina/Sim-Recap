using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Image healthbar;

    public void UpdateHealthbar(int health, int maxHealth)
    {
        healthbar.fillAmount = (float) health / maxHealth;
    }
}
