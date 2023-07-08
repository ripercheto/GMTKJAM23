using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class BaseItemData : ScriptableObject
{
    public bool TryPlayerUse(ItemPickup pickup)
    {
        if (!MainCharacters.TryGetPlayer(out var player))
        {
            return false;
        }
        return OnTryPlayerUse(player, pickup);
    }

    public bool TryGivePrincess(ItemPickup pickup)
    {
        if (!MainCharacters.TryGetPrincess(out var princess))
        {
            return false;
        }
        return OnTryGivePrincess(princess, pickup);
    }

    public abstract bool OnTryPlayerUse(PlayerInput player, ItemPickup pickup);
    public abstract bool OnTryGivePrincess(Princess princess, ItemPickup pickup);
}