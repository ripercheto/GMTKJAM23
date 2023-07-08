using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float takeDamageCooldown = 0.1f;
    public float maxHealth = 100;
    public FlashController flashController;

    [ShowInInspector, ReadOnly]
    private float currentHealth;
    public event Action onDeath;
    public event Action<float> onHealthChaned;
    public bool IsAlive => currentHealth > 0;

    public bool invincible;
    private float takeDamageTime;

    private void Awake()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        onHealthChaned?.Invoke(Mathf.Clamp01(currentHealth / maxHealth));
        flashController.ResetFlash();
    }

    public void TakeDamage(float amount)
    {
        if (!IsAlive)
        {
            return;
        }

        if (invincible)
        {
            return;
        }

        if (Time.time < takeDamageTime)
        {
            return;
        }

        currentHealth -= amount;
        onHealthChaned?.Invoke(Mathf.Clamp01(currentHealth / maxHealth));
        flashController.Flash(amount > 0 ? Color.white : Color.green);
        takeDamageTime = Time.time + takeDamageCooldown;

        if (currentHealth <= 0)
        {
            onDeath?.Invoke();
        }
    }
}