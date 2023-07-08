using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : GameBehaviour
{
    public float power = 5;
    public float duration = 0.1f;
    public float cooldown = 5;
    
    private float dashTime;

    public void TryRoll(Vector3 dir)
    {
        if (Time.time < dashTime)
        {
            return;
        }

        var playerLayer = LayerMask.NameToLayer("Player");
        var enemyLayer = LayerMask.NameToLayer("Enemy");

        Physics.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        health.invincible = true;
        movement.StartDash(dir, power, duration, OnDone);
        dashTime = Time.time + duration + cooldown;

        void OnDone()
        {
            health.invincible = false;
            Physics.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        }
    }
}