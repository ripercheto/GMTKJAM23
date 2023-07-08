using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : GameBehaviour
{
    public static PlayerInput instance;

    public PlayerPickUpController pickUpController;
    private bool princessDead;

    private void Awake()
    {
        MainCharacters.AddToMainCharacters(this);
        health.onDeath += () => Destroy(gameObject);
        instance = this;
    }

    private void Start()
    {
        Princess.instance.health.onDeath += OnPrincessDeath;
    }

    private void OnPrincessDeath()
    {
        princessDead = true;
        movement.UpdateDesiredVelocity(Vector3.zero);
    }

    void Update()
    {
        if (princessDead)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
        {
            pickUpController.TryUseItem();
        }
        
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        movement.UpdateDesiredVelocity(playerInput);
    }
}