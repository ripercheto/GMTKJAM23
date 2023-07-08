using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public EnemyType type = EnemyType.Bat;
    public MinMaxFloat spawnDelay;
    public MinMaxFloat spawnCooldown;
    public MinMaxInt spawnAmount;
    public float spawnRange = 5;

    private float spawnTime;
    private EnemyPool pool;

    private void Awake()
    {
        spawnTime = Time.time + spawnDelay.GetRandom();
    }

    private void Start()
    {
        pool = EnemyPool.Get(type);
        MainCharacters.onMainDeath += OnMainDeath;
    }

    private void OnDestroy()
    {
        MainCharacters.onMainDeath -= OnMainDeath;
    }

    private void OnMainDeath()
    {
        enabled = false;
    }

    private void Update()
    {
        if (Time.time < spawnTime)
        {
            return;
        }
        SetSpawnTime();
        var amount = spawnAmount.GetRandom();
        for (var i = 0; i < amount; i++)
        {
            var random = Random.insideUnitCircle * spawnRange;
            pool.Activate(transform.position + random.To3D());
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }

    private void SetSpawnTime()
    {
        spawnTime = Time.time + spawnCooldown.GetRandom();
    }
}