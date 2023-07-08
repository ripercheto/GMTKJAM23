using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : GameBehaviour
{
    private WeaponData data;
    private Weapon weapon;
    private float allowedHitTime;
    private ItemPickup lastPickup;
    public bool HasWeapon => data != null;
    
    public bool TryEquipWeapon(ItemPickup pickup)
    {
        if (pickup.itemData is not WeaponData weaponData)
        {
            return false;
        }
        if (lastPickup != null)
        {
            lastPickup.transform.position = transform.position;
            lastPickup.gameObject.SetActive(true);
        }
        data = weaponData;
        lastPickup = pickup;
        lastPickup.gameObject.SetActive(false);
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
        StartCoroutine(data.PerformAttack(movement.body, dir));
        attacker.onDeath += OnAttackerDied;

        void OnAttackerDied()
        {
            data.CleanUp();
            StopAllCoroutines();
        }

    }
}