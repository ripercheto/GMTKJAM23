using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement), typeof(Health))]
public class GameBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Movement movement;
    [HideInInspector]
    public Health health;
    private void OnValidate()
    {
        movement = GetComponent<Movement>();
        health = GetComponent<Health>();
    }
}