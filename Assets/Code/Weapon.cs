using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Rigidbody body;
    public readonly List<Health> healths = new();
    public Action<Health> actionOnEnter;

    private int hitCount;
    public int HitCount => hitCount;

    private void OnTriggerEnter(Collider other)
    {
        var health = other.gameObject.GetComponent<Health>();
        if (healths.Contains(health))
        {
            return;
        }

        actionOnEnter?.Invoke(health);
        healths.Add(health);
        hitCount++;
    }

    public void Clear()
    {
        healths.Clear();
    }
}