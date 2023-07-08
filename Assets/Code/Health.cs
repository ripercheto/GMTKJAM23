using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public FlashController flashController;
    
    [ShowInInspector, ReadOnly]
    private float currentHealth;
    
    public event Action onDeath;
    public bool IsAlive => currentHealth > 0;

    private void Awake()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        flashController.ResetFlash();   
    }

    public void TakeDamage(float amount)
    {
        if (!IsAlive)
        {
            return;
        }
        currentHealth -= amount;
        flashController.Flash();
        if (currentHealth <= 0)
        {
            onDeath?.Invoke();
        }
    }
}