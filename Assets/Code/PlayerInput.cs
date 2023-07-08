using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : GameBehaviour
{
    public static PlayerInput instance;

    private bool princessDead;

    private void Awake()
    {
        health.onDeath += () => Destroy(gameObject);
        Princess.instance.health.onDeath += OnPrincessDeath;
        instance = this;
    }

    private void OnPrincessDeath()
    {
        princessDead = true;
    }

    void Update()
    {
        if (princessDead)
        {
            return;
        }
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        movement.UpdateDesiredVelocity(playerInput);
    }
}