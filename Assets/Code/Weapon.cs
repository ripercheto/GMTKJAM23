using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private List<Health> healths = new List<Health>();

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        var health = collision.gameObject.GetComponent<Health>();
        if (healths.Contains(health))
        {
            return;
        }

        healths.Add(health);
    }
}