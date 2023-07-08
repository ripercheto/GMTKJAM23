using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public void Drop()
    {
        transform.SetParent(null, true);
        transform.position = transform.GetFlatPosition();
    }

    public bool TryUse()
    {
        return false;
    }
}