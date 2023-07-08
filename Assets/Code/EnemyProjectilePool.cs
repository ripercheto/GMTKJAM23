using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectilePool : PrefabPool<EnemyProjectile>
{
    public static EnemyProjectilePool instance;

    private void Awake()
    {
        instance = this;
    }
}