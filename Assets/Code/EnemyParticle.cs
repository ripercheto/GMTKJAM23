using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticle : MonoBehaviour
{
    public ParticleSystem particle;
    public ParticleType type;

    void OnParticleSystemStopped()
    {
        ParticlePool.Get(type).Deactivate(particle);
    }
}
