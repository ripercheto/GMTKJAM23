using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public MinMaxFloat spawnDelay;
    public MinMaxFloat spawnCooldown;
    public MinMaxInt spawnAmount;
    public float spawnRange = 5;

    public Enemy enemyPrefab;

    private float spawnTime;

    private void Awake()
    {
        spawnTime = Time.time + spawnDelay.GetRandom();
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