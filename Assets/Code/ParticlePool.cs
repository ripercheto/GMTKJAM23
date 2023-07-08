using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : PrefabPool<ParticleSystem>
{
    public static ParticlePool instance;

    private void Awake()
    {
        instance = this;
    }
}