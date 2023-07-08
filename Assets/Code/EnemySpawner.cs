using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public MinMaxFloat spawnDelay;
    public MinMaxFloat spawnCooldown;
    public MinMaxInt spawnAmount;
    public float spawnRange = 5;

    private float spawnTime;

    private void Awake()
    {
        spawnTime = Time.time + spawnDelay.GetRandom();
    }

    private void Start()
    {
        MainCharacters.onMainDeath += () => enabled = false;
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
            EnemyPool.instance.Activate(transform.position + random.To3D());
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