using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType
{
    Death,
    Spawn,
    Projectile,
}

public class ParticlePool : PrefabPool<ParticleSystem>
{
    public ParticleType type;
    private static ParticlePool spawn;
    private static ParticlePool death;
    private static ParticlePool projectile;

    public static ParticlePool Get(ParticleType type)
    {
        switch (type)
        {
            case ParticleType.Death:
                return death;
            case ParticleType.Spawn:
                return spawn;
            case ParticleType.Projectile:
                return projectile;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    private void Awake()
    {
        switch (type)
        {
            case ParticleType.Death:
                death = this;
                break;
            case ParticleType.Spawn:
                spawn = this;
                break;
            case ParticleType.Projectile:
                projectile = this;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}