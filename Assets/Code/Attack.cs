using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MovementBehaviour
{
    public GameObject attackPrefab;

    public float power = 10f;
    public float duration = 0.1f;
    public float attackAngle = 180f;
    public float cooldown = 0.5f;

    private GameObject attackObject;
    private float allowedHitTime;

    public void TryPerformAttack(Vector3 dir)
    {
        if (Time.time < allowedHitTime)
        {
            return;
        }
        allowedHitTime = Time.time + duration + cooldown;

        movement.StartDash(dir, power, duration, null);
        StartCoroutine(SwingWeapon(dir, duration, attackAngle));
    }

    private IEnumerator SwingWeapon(Vector3 direction, float duration, float arcAngle)
    {
        var halfAngle = arcAngle * 0.5f;
        var startRotation = Quaternion.LookRotation(direction, Vector3.up);
        attackObject = Instantiate(attackPrefab, transform.position, startRotation * Quaternion.AngleAxis(-halfAngle, Vector3.up));
        var t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            yield return null;
            var angle = Mathf.Lerp(-halfAngle, halfAngle, t);
            attackObject.transform.SetPositionAndRotation(transform.position, startRotation * Quaternion.AngleAxis(angle, Vector3.up));
        }
        Destroy(attackObject);
    }
}