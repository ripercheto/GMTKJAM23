using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : GameBehaviour
{
    public Weapon weaponPrefab;

    public float damage = 50f;
    public float duration = 0.1f;
    public float attackAngle = 180f;
    public float cooldown = 0.5f;

    private Weapon weapon;
    private float allowedHitTime;

    public void TryPerformAttack(Vector3 dir)
    {
        if (Time.time < allowedHitTime)
        {
            return;
        }
        allowedHitTime = Time.time + duration + cooldown;

        //movement.StartDash(dir, power, duration, null);
        StartCoroutine(SwingWeapon(dir, duration, attackAngle));
    }

    private IEnumerator SwingWeapon(Vector3 direction, float duration, float arcAngle)
    {
        var halfAngle = arcAngle * 0.5f;
        var startRotation = Quaternion.LookRotation(direction, Vector3.up);
        weapon = Instantiate(weaponPrefab, transform.position, startRotation * Quaternion.AngleAxis(-halfAngle, Vector3.up));
        weapon.actionOnEnter = (x) => x.TakeDamage(damage);
        var t = 0f;
        while (t < 1f)
        { 
            t += Time.fixedDeltaTime / duration;
            yield return new WaitForFixedUpdate();
            var angle = Mathf.Lerp(-halfAngle, halfAngle, t);
            weapon.body.MovePosition(movement.body.position);
            weapon.body.MoveRotation(startRotation * Quaternion.AngleAxis(angle, Vector3.up));
        }
        Destroy(weapon.gameObject);
    }
}