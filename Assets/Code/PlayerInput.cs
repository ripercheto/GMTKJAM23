using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MovementBehaviour
{
    void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        movement.UpdateDesiredVelocity(playerInput);
    }
}