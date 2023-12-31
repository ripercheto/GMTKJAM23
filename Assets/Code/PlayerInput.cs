using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : GameBehaviour
{
    public static PlayerInput instance;

    public GameObject deathFX;
    public PlayerPickUpController pickUpController;
    public PlayerRoll roll;
    private bool princessDead;

    private void Awake()
    {
        MainCharacters.AddToMainCharacters(this);
        health.onDeath += OnDeath;
        instance = this;
    }

    private void OnDeath()
    {
        Instantiate(deathFX, transform.GetFlatPosition(), Quaternion.identity);
        Destroy(gameObject);
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
        var desiredDir = movement.desiredVelocity.To3D().normalized;
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
        {
            pickUpController.TryPlayerUseItem(desiredDir);
        }
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        movement.UpdateDesiredVelocity(playerInput);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            roll.TryRoll(desiredDir);
        }
    }
}