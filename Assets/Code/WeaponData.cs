using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : BaseItemData
{
    public Weapon weaponPrefab;

    public float damage = 50f;
    public float duration = 0.5f;
    public float attackAngle = 180f;
    public float cooldown = 1f;

    private Weapon weapon;

    public IEnumerator PerformAttack(Rigidbody body, Vector3 dir, Action onDone)
    {
        var halfAngle = attackAngle * 0.5f;
        var startRotation = Quaternion.LookRotation(dir, Vector3.up);

        weapon = Instantiate(weaponPrefab, body.position, startRotation * Quaternion.AngleAxis(-halfAngle, Vector3.up));
        weapon.actionOnEnter = (x) => x.TakeDamage(damage);

        var t = 0f;
        while (t < 1f)
        {
            t += Time.fixedDeltaTime / duration;
            yield return new WaitForFixedUpdate();
            var angle = Mathf.Lerp(-halfAngle, halfAngle, t);
            weapon.body.MovePosition(body.position);
            weapon.body.MoveRotation(startRotation * Quaternion.AngleAxis(angle, Vector3.up));
        }

        Destroy(weapon.gameObject);
        onDone?.Invoke();
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