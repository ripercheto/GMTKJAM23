using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpController : GameBehaviour
{
    public Transform socket;
    public float pickUpCooldown = 0.1f;
    private float pickUpTime;
    private ItemPickup pickedUpItem;
    public AudioSource pickUpSoundSource;
    public AudioClip pickupSound;
    public AudioClip dropSound;

    private void Awake()
    {
        var items = FindObjectsOfType<ItemPickup>();
        foreach (var item in items)
        {
            if (item.itemData is WeaponData weaponData)
            {
                item.durability = weaponData.durability;
            }
        }
    }

    public void PickUp(ItemPickup itemPickup)
    {
        DropItem(Vector3.zero);
        itemPickup.PickUp();
        pickUpSoundSource.clip = pickupSound;
        pickUpSoundSource.Play();

        pickedUpItem = itemPickup;

        var itemTransform = pickedUpItem.transform;
        itemTransform.SetParent(socket);
        itemTransform.localPosition = Vector3.zero;
        itemTransform.localRotation = Quaternion.identity;
    }

    public void TryPlayerUseItem(Vector3 dir)
    {
        if (pickedUpItem != null)
        {
            if (pickedUpItem.TryGivePlayer())
            {
                DestroyItem();
            }
            else
            {
                DropItem(dir);
                pickUpSoundSource.clip = dropSound;
                pickUpSoundSource.Play();
            }
        }
    }

    public void DestroyItem()
    {
        if (pickedUpItem == null)
        {
            return;
        }
        Destroy(pickedUpItem.gameObject);
    }

    public void DropItem(Vector3 dir)
    {
        if (pickedUpItem == null)
        {
            return;
        }

        pickedUpItem.Drop(movement.velocity, dir);
        pickedUpItem = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var princess = collision.gameObject.GetComponent<Princess>();
        if (princess == null)
        {
            return;
        }
        if (pickedUpItem == null)
        {
            return;
        }
        if (pickedUpItem.TryGivePrincess())
        {
            DestroyItem();
        }
        else
        {
            DropItem(Vector3.zero);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Time.time < pickUpTime)
        {
            return;
        }
        var pickUp = collision.gameObject.GetComponent<ItemPickup>();
        if (pickUp == null)
        {
            return;
        }

        PickUp(pickUp);
        pickUpTime = Time.time + pickUpCooldown;
    }
}