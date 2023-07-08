using System;
using System.Collections.Generic;
using UnityEngine;

public static class MainCharacters
{
    public static List<Transform> targets = new List<Transform>();
    public static event Action onMainDeath;

    public static void AddToMainCharacters(GameBehaviour gameBehaviour)
    {
        gameBehaviour.health.onDeath += OnDeath;
        targets.Add(gameBehaviour.transform);

        void OnDeath()
        {
            onMainDeath?.Invoke();
            targets.Remove(gameBehaviour.transform);
        }
    }
}