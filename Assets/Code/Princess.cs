using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : GameBehaviour
{
    public static Princess instance;

    public Attack attack;
    public PrincessRange range;

    public float attackDistance = 2;
    public float distanceFromTarget = 1;
    public Transform[] wayPoints;

    private Transform CurrentWaypoint => wayPoints[wayPointIndex];
    private int wayPointIndex;
    private Vector3 lastPosition;

    private void Awake()
    {
        instance = this;
        health.onDeath += OnDeath;
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        var pos = transform.GetFlatPosition();
        var targetPos = GetTargetPosition();
        if (Vector3.Distance(pos, targetPos) > distanceFromTarget)
        {
            var dir = targetPos - pos;
            movement.UpdateDesiredVelocity(dir);
        }
        else
        {
            wayPointIndex++;
            if (wayPointIndex >= wayPoints.Length)
            {
                wayPointIndex = 0;
            }
        }

        var attackDir = targetPos - pos;
        attack.TryPerformAttack(health, attackDir);

        Vector3 GetTargetPosition()
        {
            if (range.HasEnemiesInRange)
            {
                var enemyPos = range.CenterPosition;
                var dir = enemyPos - pos;
                var enemyTargetPos = enemyPos - dir.normalized * attackDistance;
                Debug.DrawLine(pos, enemyTargetPos);
                return enemyTargetPos;
            }

            return CurrentWaypoint.GetFlatPosition();
        }
    }
}