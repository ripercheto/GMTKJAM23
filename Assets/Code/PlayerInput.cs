using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : GameBehaviour
{
    public static PlayerInput instance;

    public PlayerPickUpController pickUpController;
    public PlayerRoll roll;
    private bool princessDead;

    private void Awake()
    {
        MainCharacters.AddToMainCharacters(this);
        health.onDeath += () => Destroy(gameObject);
        instance = this;
    }

    private void Start()
    {
        if (!MainCharacters.TryGetPrincess(out var princess))
        {
            return;
        }
        princess.health.onDeath += OnPrincessDeath;
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
            pickUpController.TryPlayerUseItem();
        }
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        movement.UpdateDesiredVelocity(playerInput);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            roll.TryRoll(movement.desiredVelocity.To3D().normalized);
        }
    }
}