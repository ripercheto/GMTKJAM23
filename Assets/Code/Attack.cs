using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : GameBehaviour
{
    private WeaponData data;
    private Weapon weapon;
    private float allowedHitTime;
    public bool HasWeapon => data != null;

    public bool TryEquipWeapon(WeaponData weaponData)
    {
        if (data != null)
        {
            Instantiate(data.prefab, transform.position, Quaternion.identity);
        }
        data = weaponData;
        return true;
    }

    public void TryPerformAttack(Health attacker, Vector3 dir)
    {
        if (!HasWeapon)
        {
            return;
        }

        if (Time.time < allowedHitTime)
        {
            return;
        }
        allowedHitTime = Time.time + data.duration + data.cooldown;
        StartCoroutine(data.PerformAttack(movement.body, dir, OnDone));
        
        attacker.onDeath += OnAttackerDied;
        void OnAttackerDied()
        {
            data.CleanUp();
            StopAllCoroutines();
        }
        void OnDone()
        {
            attacker.onDeath -= OnAttackerDied;
        }
    }
}