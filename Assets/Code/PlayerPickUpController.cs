using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpController : MonoBehaviour
{
    public Transform socket;
    private float pickUpTime;
    private ItemPickup pickedUpItem;

    public void PickUp(ItemPickup itemPickup)
    {
        DropItem();

        pickedUpItem = itemPickup;

        var itemTransform = pickedUpItem.transform;
        itemTransform.SetParent(socket);
        itemTransform.localPosition = Vector3.zero;
        itemTransform.localRotation = Quaternion.identity;
    }

    public void TryPlayerUseItem()
    {
        if (pickedUpItem != null)
        {
            if (pickedUpItem.TryGivePlayer())
            {
                DestroyItem();
            }
            else
            {
                DropItem();
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

    public void DropItem()
    {
        if (pickedUpItem == null)
        {
            return;
        }

        pickedUpItem.Drop();
        pickedUpItem = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var princess = collision.gameObject.GetComponent<Princess>();
        if (princess != null)
        {
            if (pickedUpItem != null)
            {
                if (pickedUpItem.TryGivePrincess())
                {
                    DestroyItem();
                }
                else
                {
                    DropItem();
                }
            }

            return;
        }
        var pickUp = collision.gameObject.GetComponent<ItemPickup>();
        if (pickUp == null)
        {
            return;
        }

        PickUp(pickUp);
    }
}