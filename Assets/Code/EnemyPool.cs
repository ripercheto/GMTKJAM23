using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : PrefabPool<Enemy>
{
    public static EnemyPool instance;

    private void Awake()
    {
        instance = this;
    }
}