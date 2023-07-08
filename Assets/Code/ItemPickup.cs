using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public BaseItemData itemData;
    public void Drop()
    {
        transform.SetParent(null, true);
        transform.position = transform.GetFlatPosition();
    }

    public bool TryGivePlayer()
    {
        return itemData.TryPlayerUse();
    }

    public bool TryGivePrincess()
    {
        return itemData.TryGivePrincess();
    }
}