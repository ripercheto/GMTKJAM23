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

    private Dictionary<WeaponData, float> durability = new();
    public Action<float> onDurabilityChanged;
    public float DurabilityAlpha => data == null ? 0 : durability[data] / data.durability;

    private void Start()
    {
        onDurabilityChanged?.Invoke(HasWeapon ? 1 : 0);
    }

    public bool TryEquipWeapon(WeaponData weaponData)
    {
        //initialize durability
        if (!durability.ContainsKey(weaponData))
        {
            durability.Add(weaponData, weaponData.durability);
        }
        if (data == weaponData)
        {
            //same weapon
            durability[data] = data.durability;
        }
        else
        {
            //different, drop
            if (data != null)
            {
                Instantiate(data.prefab, transform.position, Quaternion.identity);
            }
        }

        data = weaponData;
        OnDurabilityChanged();
        return true;
    }

    public void TryPerformAttack(Health attacker, PrincessRange range)
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
        StartCoroutine(data.PerformAttack(movement.body, range, OnDone));

        attacker.onDeath += OnAttackerDied;

        void OnAttackerDied()
        {
            data.CleanUp();
            StopAllCoroutines();
        }

        void OnDone(bool didHit)
        {
            attacker.onDeath -= OnAttackerDied;

            if (!didHit)
            {
                return;
            }
            durability[data] -= data.durabilityPerHit;
            //destroy if out of durability
            if (durability[data] <= 0)
            {
                durability.Remove(data);
                data = null;
            }
            OnDurabilityChanged();
        }
    }

    private void OnDurabilityChanged()
    {
        onDurabilityChanged?.Invoke(DurabilityAlpha);
    }
}