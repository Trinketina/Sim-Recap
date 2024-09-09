using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Healthbar", menuName = "SO/HealthHandler")]
public class HealthSystem : ScriptableObject
{
    public int MaxHealth { get { return maxHealth; } }
    [SerializeField] private int maxHealth = 100;

    public int Health { get { return health; } set { health = value > 0 ? value < maxHealth ? value : maxHealth : 0; } }

    [SerializeField] private int health;
}
