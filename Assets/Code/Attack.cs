using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MovementBehaviour
{
    public float power = 10f;
    public float duration = 0.1f;
    public float cooldown = 0.5f;

    private float allowedHitTime;
    public void TryPerformAttack(Vector3 dir)
    {
        if (Time.time < allowedHitTime)
        {
            return;
        }
        movement.StartDash(dir, power, duration);
        allowedHitTime = Time.time + cooldown;
    }
}
