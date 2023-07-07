using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MovementBehaviour
{
    public float power = 10f;
    public float duration = 0.1f;
    public float holdDuration = 0.1f;
    public float cooldown = 0.5f;

    private float allowedHitTime;

    public void TryPerformAttack(Vector3 dir)
    {
        if (Time.time < allowedHitTime)
        {
            return;
        }
        allowedHitTime = Time.time + duration + holdDuration + cooldown;

        movement.StartDash(dir, power, duration, Hold);
        void Hold()
        {
            movement.StartDash(Vector3.zero, 0, holdDuration, null);
        }
    }
}