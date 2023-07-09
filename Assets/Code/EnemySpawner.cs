using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public EnemyType type = EnemyType.Bat;
    public MinMaxFloat spawnDelay;
    public MinMaxFloat spawnCooldown;
    public float minCooldown = 3;
    public float cooldownDecreasePerSpawn = 1;

    public MinMaxInt spawnAmount;
    public float spawnRange = 5;
    public AudioSource spawnSound;

    private float spawnTime;
    private EnemyPool pool;
    private float cooldownDecrease;

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
            spawnSound.Play();
            var random = Random.insideUnitCircle * spawnRange;
            pool.Activate(transform.position + random.To3D());
        }

        cooldownDecrease += cooldownDecreasePerSpawn;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }

    private void SetSpawnTime()
    {
        var cooldown = Mathf.Max(minCooldown, spawnCooldown.GetRandom() - cooldownDecrease);
        spawnTime = Time.time + cooldown;
    }
}