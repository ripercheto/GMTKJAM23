using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpController : MonoBehaviour
{
    public Transform socket;

    private ItemPickup pickedUpItem;

    public void PickUp(ItemPickup itemPickup)
    {
        if (pickedUpItem != null)
        {
            pickedUpItem.Drop();
        }
        pickedUpItem = itemPickup;

        var itemTransform = pickedUpItem.transform;
        itemTransform.SetParent(socket);
        itemTransform.localPosition = Vector3.zero;
        itemTransform.localRotation = Quaternion.identity;
    }

    public void TryUseItem()
    {
        if (pickedUpItem == null)
        {
            return;
        }

        if (pickedUpItem.TryUse())
        {
            //destroy
        }
        else
        {
            pickedUpItem.Drop();
            pickedUpItem = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var pickUp = collision.gameObject.GetComponent<ItemPickup>();
        if (pickUp == null)
        {
            return;
        }

        PickUp(pickUp);
    }
}