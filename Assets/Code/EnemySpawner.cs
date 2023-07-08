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
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        var amount = spawnAmount.GetRandom();
        for (var i = 0; i < amount; i++)
        {
            var random = Random.insideUnitCircle * spawnRange;
            Instantiate(enemyPrefab, transform.position + random.To3D(), Quaternion.identity);
            yield return null;
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