using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Rigidbody body;
    public float speed = 1f;
    public float aliveDuration = 5f;

    private float damage = 10;
    private float deathTime;

    public void Launch(Vector3 dir, float attackDamage)
    {
        damage = attackDamage;
        body.AddForce(dir * speed, ForceMode.VelocityChange);
    }

    private void OnEnable()
    {
        deathTime = Time.time + aliveDuration;
    }

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<Health>();
        if (health == null)
        {
            return;
        }
        health.TakeDamage(damage);
        OnDeath();
    }

    private void Update()
    {
        if (Time.time > deathTime)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        ParticlePool.Get(ParticleType.Projectile).Activate(transform.position, (x) => x.Play());
        EnemyProjectilePool.instance.Deactivate(this);
    }
}