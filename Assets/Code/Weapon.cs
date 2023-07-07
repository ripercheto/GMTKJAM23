using System;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    public Rigidbody body;
    public float damage = 10;
    private readonly List<Health> healths = new();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        var health = other.gameObject.GetComponent<Health>();
        if (healths.Contains(health))
        {
            return;
        }

        health.TakeDamage(damage);
        healths.Add(health);
    }
}