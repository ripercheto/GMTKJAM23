using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessRange : MonoBehaviour
{
    public bool HasEnemiesInRange => enemies.Count > 0;

    public Vector3 CenterPosition
    {
        get
        {
            var center = Vector3.zero;
            for (var i = 0; i < enemies.Count; i++)
            {
                var enemy = enemies[i];
                if (enemy == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                    continue;
                }
                center = Vector3.Lerp(center, enemy.position, 0.5f);
            }
            return center;
        }
    }

    public readonly List<Transform> enemies = new();

    private void OnTriggerEnter(Collider other)
    {
        enemies.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        enemies.Remove(other.transform);
    }
}
