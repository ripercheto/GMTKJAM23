using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public FlashController flashController;
    private float currentHealth;
    public event Action onDeath;
    public bool IsAlive => currentHealth > 0;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (!IsAlive)
        {
            return;
        }
        currentHealth -= amount;
        flashController.Flash();
        if (currentHealth < 0)
        {
            onDeath?.Invoke();
        }
    }
}