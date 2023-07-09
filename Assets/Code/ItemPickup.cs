using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private float throwForce = 50;
    public float throwPickUpTime = 1;
    public Rigidbody body;
    public float durability;
    public BaseItemData itemData;

    private Coroutine delayedEnable;
    private bool playerThrew;
    
    public void PickUp()
    {
        body.AddForce(Vector3.zero, ForceMode.VelocityChange);
        body.isKinematic = true;
    }

    public void Drop(Vector3 currentVel, Vector3 dir)
    {
        var playerLayer = LayerMask.NameToLayer("Player");
        var pickupLayer = LayerMask.NameToLayer("Pickup");
        delayedEnable = StartCoroutine(DelayedEnable(playerLayer, pickupLayer));
        
        body.isKinematic = false;
        body.AddForce(currentVel + dir * throwForce, ForceMode.VelocityChange);

        playerThrew = dir.sqrMagnitude > 0;
        if (playerThrew)
        {
            //player tried to throw
            StartCoroutine(DisablePickup());
        }

        transform.SetParent(null, true);
        transform.position = transform.GetFlatPosition();
    }

    IEnumerator DisablePickup()
    {
        yield return new WaitForSeconds(throwPickUpTime);
        playerThrew = false;
    }

    IEnumerator DelayedEnable(int layer, int layer2)
    {
        Physics.IgnoreLayerCollision(layer, layer2, true);
        yield return new WaitForSeconds(0.5f);
        Physics.IgnoreLayerCollision(layer, layer2, false);
        delayedEnable = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!playerThrew)
        {
            return;
        }
        var princess = collision.gameObject.GetComponent<Princess>();
        if (princess == null)
        {
            return;
        }

        if (TryGivePrincess())
        {
            if (delayedEnable != null)
            {
                var playerLayer = LayerMask.NameToLayer("Player");
                var pickupLayer = LayerMask.NameToLayer("Pickup");
                Physics.IgnoreLayerCollision(playerLayer, pickupLayer, false);
            }
            Destroy(gameObject);
        }
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