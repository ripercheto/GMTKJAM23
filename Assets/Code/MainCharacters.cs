using System;
using System.Collections.Generic;
using UnityEngine;

public static class MainCharacters
{
    public static List<Transform> targets = new List<Transform>();
    public static event Action onMainDeath;

    public static Camera Camera
    {
        get
        {
            if (cam == null)
            {
                cam = Camera.main;
            }
            return cam;
        }
    }

    private static Camera cam;

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

    public static bool TryGetPlayer(out PlayerInput player)
    {
        if (PlayerInput.instance == null)
        {
            player = null;
            return false;
        }
        player = PlayerInput.instance;
        return player;
    }
    
    public static bool TryGetPrincess(out Princess princess)
    {
        if (Princess.instance == null)
        {
            princess = null;
            return false;
        }
        princess = Princess.instance;
        return princess;
    }
}