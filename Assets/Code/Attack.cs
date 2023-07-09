using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : GameBehaviour
{
    public WeaponData data;
    private Weapon weapon;
    private float allowedHitTime;
    public bool HasWeapon => data != null;
    public AudioSource soundSource;
    public AudioClip weaponPickupSound;

    private float durability;
    public Action<float> onDurabilityChanged;
    public float DurabilityAlpha => data == null ? 0 : durability / data.durability;

    private void Start()
    {
        onDurabilityChanged?.Invoke(HasWeapon ? 1 : 0);
    }

    public bool TryEquipWeapon(WeaponData weaponData, ItemPickup pickup)
    {
        soundSource.clip = weaponPickupSound;
        soundSource.Play();

        durability = pickup.durability;
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
            durability -= data.durabilityPerHit;
            //destroy if out of durability
            if (durability <= 0)
            {
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