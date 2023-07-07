using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class MovementBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Movement movement;

    private void OnValidate()
    {
        movement = GetComponent<Movement>();
    }
}