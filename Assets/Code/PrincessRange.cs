using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PrincessRange : MonoBehaviour
{
    public bool HasEnemiesInRange
    {
        get
        {
            enemies.RemoveAll(x => x == null);
            return enemies.Count > 0;
        }
    }

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
                center = Vector3.Lerp(center, enemy.transform.position, 0.5f);
            }
            return center;
        }
    }

    public Vector3 Closest => transform.GetClosestTarget(enemies).position;

    [ShowInInspector]
    public readonly List<Enemy> enemies = new();

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy == null)
        {
            return;
        }
        enemies.Add(enemy);
    }

    private void OnTriggerExit(Collider other)
    {
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy == null)
        {
            return;
        }
        enemies.Add(enemy);
    }
}