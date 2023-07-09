using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int killsPerItem = 20;
    public float distanceFromPrincess = 6;
    public BaseItemData[] itemsToSpawn;

    public float delayAfterCharacterDies = 1;
    private bool gameOver;
    private int enemiesKilled;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        enemiesKilled = 0;
        MainCharacters.onMainDeath += OnMainDeath;
    }

    private void OnMainDeath()
    {
        if (gameOver)
        {
            return;
        }

        gameOver = true;
        StartCoroutine(WaitForSeconds());

        IEnumerator WaitForSeconds()
        {
            yield return new WaitForSeconds(delayAfterCharacterDies);
            SceneManager.LoadScene("Game Over");
        }
    }

    public void OnEnemyKilled(Vector3 position)
    {
        if (!MainCharacters.TryGetPrincess(out var princess))
        {
            return;
        }
        enemiesKilled++;
        if (enemiesKilled % killsPerItem == 0)
        {
            var princessPos = princess.transform.position;
            var dirFromPrincess = (position - princessPos).normalized;
            var pos = (princessPos + dirFromPrincess * distanceFromPrincess).GetFlatPosition();
            var item = itemsToSpawn[Random.Range(0, itemsToSpawn.Length)];
            Instantiate(item.prefab, pos, Quaternion.identity);
            ParticlePool.Get(ParticleType.Spawn).Activate(pos, (x) => x.Play());
        }
    }
}