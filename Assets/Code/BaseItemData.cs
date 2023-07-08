using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class BaseItemData : ScriptableObject
{
    public ItemPickup prefab;
    
    public bool TryPlayerUse()
    {
        if (!MainCharacters.TryGetPlayer(out var player))
        {
            return false;
        }
        return OnTryPlayerUse(player);
    }

    public bool TryGivePrincess()
    {
        if (!MainCharacters.TryGetPrincess(out var princess))
        {
            return false;
        }
        return OnTryGivePrincess(princess);
    }

    public abstract bool OnTryPlayerUse(PlayerInput player);
    public abstract bool OnTryGivePrincess(Princess princess);
}