using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private float throwForce = 50;
    public Rigidbody body;
    public float durability;
    public BaseItemData itemData;

    public void PickUp()
    {
        body.AddForce(Vector3.zero, ForceMode.VelocityChange);
        body.isKinematic = true;
    }

    public void Drop(Vector3 currentVel, Vector3 dir)
    {
        var playerLayer = LayerMask.NameToLayer("Player");
        var pickupLayer = LayerMask.NameToLayer("Pickup");
        StartCoroutine(DelayedEnable(playerLayer, pickupLayer));
        
        body.isKinematic = false;
        body.AddForce(currentVel + dir * throwForce, ForceMode.VelocityChange);

        transform.SetParent(null, true);
        transform.position = transform.GetFlatPosition();
    }

    IEnumerator DelayedEnable(int layer, int layer2)
    {
        Physics.IgnoreLayerCollision(layer, layer2, true);
        yield return new WaitForSeconds(0.5f);
        Physics.IgnoreLayerCollision(layer, layer2, false);
    }

    public bool TryGivePlayer()
    {
        return itemData.TryPlayerUse(this);
    }

    public bool TryGivePrincess()
    {
        return itemData.TryGivePrincess(this);
    }
}