using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotionItemData : BaseItemData
{
    public float healAmount = 10;

    public override bool OnTryPlayerUse(PlayerInput player)
    {
        player.health.TakeDamage(-healAmount);
        return true;
    }

    public override bool OnTryGivePrincess(Princess princess)
    {
        princess.health.TakeDamage(-healAmount);
        return true;
    }
}