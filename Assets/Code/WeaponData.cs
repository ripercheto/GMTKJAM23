using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WeaponData : BaseItemData
{
    public Weapon weaponPrefab;

    public float damage = 50f;
    public float cooldown = 1f;
    public float duration = 0.5f;
    [Space, Min(0)]
    public float moveAmount = 1f;
    [HideIf(nameof(moveAmount), 0)]
    public AnimationCurve moveCurve;
    public float resetHitEnemiesPoint = 1f;

    [Space]
    public float attackAngle = 180f;
    [Space]
    public float durability = 50f;
    public float durabilityPerHit = 5f;

    private Weapon weapon;

    public IEnumerator PerformAttack(Rigidbody body, Vector3 dir, Action<bool> onDone)
    {
        dir.Normalize();
        var halfAngle = attackAngle * 0.5f;
        var startRotation = Quaternion.LookRotation(dir, Vector3.up);

        weapon = Instantiate(weaponPrefab, body.position, startRotation * Quaternion.AngleAxis(-halfAngle, Vector3.up));
        weapon.actionOnEnter = (x) => x.TakeDamage(damage);
        var endPos = dir * moveAmount;
        var t = 0f;
        var reset = false;
        while (t < 1f)
        {
            t += Time.fixedDeltaTime / duration;
            yield return new WaitForFixedUpdate();
            var angle = Mathf.Lerp(-halfAngle, halfAngle, t);
            var pos = Vector3.Lerp(Vector3.zero, endPos, moveCurve.Evaluate(t));
            weapon.body.MovePosition(body.position + pos);
            weapon.body.MoveRotation(startRotation * Quaternion.AngleAxis(angle, Vector3.up));
            if (!reset && t >= resetHitEnemiesPoint)
            {
                reset = true;
                weapon.Clear();
            }
        }

        onDone?.Invoke(weapon.HitCount > 0);
        Destroy(weapon.gameObject);
    }

    public void CleanUp()
    {
        if (weapon == null)
        {
            return;
        }
        if (weapon.gameObject != null)
        {
            Destroy(weapon.gameObject);
        }
    }

    public override bool OnTryPlayerUse(PlayerInput player)
    {
        return false;
    }

    public override bool OnTryGivePrincess(Princess princess)
    {
        return princess.attack.TryEquipWeapon(this);
    }
}