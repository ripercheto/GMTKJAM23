using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WeaponData : BaseItemData
{
    public Weapon weaponPrefab;

    public bool melee = true;
    public float damage = 50f;
    public float attackDistance = 2;
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

    public Vector3 GetPosRanged(PrincessRange range)
    {
        var closestRangedDistance = float.MaxValue;
        var closesRangedPos = Vector3.zero;

        var closestMeleeDistance = float.MaxValue;
        var closesMeleePos = Vector3.zero;

        foreach (var enemy in range.enemies)
        {
            var dist = Vector3.Distance(enemy.transform.position, range.transform.position);
            if (enemy.useProjectile)
            {
                Debug.DrawLine(range.transform.position, enemy.transform.position, Color.blue, 2);
                if (dist < closestRangedDistance)
                {
                    closestRangedDistance = dist;
                    closesRangedPos = enemy.transform.position;
                }
            }
            else
            {
                if (dist < closestMeleeDistance)
                {
                    closestMeleeDistance = dist;
                    closesMeleePos = enemy.transform.position;
                }
            }

        }
        if (closesRangedPos == Vector3.zero)
        {
            return closesMeleePos;
        }
        return closesRangedPos;
    }

    public Vector3 GetPoMelee(PrincessRange range)
    {
        return range.CenterPosition;
    }

    public IEnumerator PerformAttack(Rigidbody body, PrincessRange range, Action<bool> onDone)
    {
        var targetPos = melee ? GetPoMelee(range) : GetPosRanged(range);
        var dir = (targetPos - body.position).GetFlatPosition().normalized;
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

    public override bool OnTryPlayerUse(PlayerInput player, ItemPickup pickup)
    {
        return false;
    }

    public override bool OnTryGivePrincess(Princess princess, ItemPickup pickup)
    {
        return princess.attack.TryEquipWeapon(this, pickup);
    }
}