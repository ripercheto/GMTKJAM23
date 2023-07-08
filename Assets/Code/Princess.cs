using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : MovementBehaviour
{
    public static Princess instance;

    public Attack attack;
    public PrincessRange range;

    public float attackDistance = 1;
    public float distanceFromWayPoint = 1;
    public Transform[] wayPoints;

    private Transform CurrentWaypoint => wayPoints[wayPointIndex];
    private int wayPointIndex;
    private Vector3 lastPosition;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {

        var pos = transform.GetFlatPosition();
        if (range.HasEnemiesInRange)
        {
            var enemyPos = range.CenterPosition;
            var dir = enemyPos - pos;
            if (Vector3.Distance(pos, enemyPos) > attackDistance)
            {
                movement.UpdateDesiredVelocity(dir);
            }
            attack.TryPerformAttack(dir.normalized);
            return;
        }

        var targetPos = CurrentWaypoint.GetFlatPosition();
        if (Vector3.Distance(pos, targetPos) > distanceFromWayPoint)
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

        var dashDir = pos - lastPosition;
        lastPosition = pos;
    }
}