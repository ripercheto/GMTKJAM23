using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : GameBehaviour
{
    enum State
    {
        Invalid,
        ComingToPlayer,
        FollowingPath,
        Attacking,
        ReturningToPath
    }

    public static Princess instance;

    public Attack attack;
    public PrincessRange range;

    public float attackDistance = 2;
    public float distanceFromTarget = 1;
    public float distanceFromWaypoint = 2;
    public float attackModeDuration = 5;
    public float attackModeCooldown = 5;
    public float strayDistance = 5;
    public Transform[] wayPoints;

    private Transform CurrentWaypoint => wayPoints[wayPointIndex];
    private int wayPointIndex;
    private Vector3 targetPosOnPath;
    private Vector3 targetPos;
    private float attackModeTime;
    private bool AttackModeOffCooldown => Time.time > attackModeTime;
    private State state;

    private void Awake()
    {
        instance = this;
        MainCharacters.AddToMainCharacters(this);
        health.onDeath += OnDeath;
        targetPosOnPath = CurrentWaypoint.position;
        state = State.FollowingPath;
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        var pos = transform.GetFlatPosition();
        var wayPointPos = CurrentWaypoint.GetFlatPosition();
        targetPosOnPath = FindClosestPointOnEdge(pos) + Vector3.ClampMagnitude(wayPointPos - targetPosOnPath, 2);
        targetPos = Vector3.zero;
        switch (state)
        {
            case State.ComingToPlayer:
                if (!MainCharacters.TryGetPlayer(out var player))
                {
                    targetPos = Vector3.zero;
                }
                else
                {
                    targetPos = player.transform.position;
                }

                if (attack.HasWeapon)
                {
                    if (range.HasEnemiesInRange && AttackModeOffCooldown)
                    {
                        state = State.Attacking;
                        attackModeTime = Time.time + attackModeDuration;
                    }
                    else
                    {
                        state = State.ReturningToPath;
                    }
                }
                break;
            case State.FollowingPath:
            {
                targetPos = targetPosOnPath;
                if (!attack.HasWeapon)
                {
                    state = State.ComingToPlayer;
                }
                else
                {
                    if (range.HasEnemiesInRange && AttackModeOffCooldown)
                    {
                        state = State.Attacking;
                        attackModeTime = Time.time + attackModeDuration;
                    }
                }

                if (Vector3.Distance(pos, wayPointPos) < distanceFromWaypoint)
                {
                    wayPointIndex++;
                    if (wayPointIndex >= wayPoints.Length)
                    {
                        wayPointIndex = 0;
                    }
                }
            }
                break;
            case State.Attacking:
            {
                if (range.HasEnemiesInRange)
                {
                    var enemyPos = range.CenterPosition;
                    var dir = enemyPos - pos;
                    var enemyTargetPos = enemyPos - dir.normalized * attackDistance;
                    Debug.DrawLine(pos, enemyTargetPos);
                    targetPos = enemyTargetPos;
                }
                else
                {
                    state = State.ReturningToPath;
                    break;
                }
                if (!attack.HasWeapon)
                {
                    state = State.ComingToPlayer;
                    break;
                }
                //distance from last point while on path
                if (Vector3.Distance(pos, targetPosOnPath) > strayDistance || Time.time > attackModeTime)
                {
                    state = State.ReturningToPath;
                    attackModeTime = Time.time + attackModeCooldown;
                }
            }
                break;
            case State.ReturningToPath:
            {
                if (!attack.HasWeapon)
                {
                    state = State.ComingToPlayer;
                }
                else if (Vector3.Distance(pos, targetPosOnPath) < distanceFromTarget)
                {
                    state = State.FollowingPath;
                }

                targetPos = targetPosOnPath;
            }
                break;
        }

        if (Vector3.Distance(pos, targetPos) > distanceFromTarget)
        {
            var dir = targetPos - pos;
            movement.UpdateDesiredVelocity(dir);
        }

        var attackDir = targetPos - pos;
        attack.TryPerformAttack(health, attackDir);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < wayPoints.Length + 1; i++)
        {
            var wayPoint = wayPoints[i % (wayPoints.Length)];
            var nexWayPoint = wayPoints[(i + 1) % (wayPoints.Length)];
            Gizmos.DrawLine(wayPoint.position, nexWayPoint.position);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(targetPosOnPath, 0.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(targetPos, 0.5f);
    }

    public Vector3 FindClosestPointOnEdge(Vector3 targetPoint)
    {
        Vector3 closestPoint = Vector3.zero;
        float closestDistance = float.MaxValue;

        int vertexCount = wayPoints.Length;

        for (int i = 0; i < vertexCount; i++)
        {
            // Get the current vertex and the next vertex (loop around to the first vertex for the last pair)
            Vector3 vertex1 = wayPoints[i].position;
            Vector3 vertex2 = wayPoints[(i + 1) % vertexCount].position;

            // Calculate the closest point on the edge formed by the current pair of vertices
            Vector3 edgeDirection = vertex2 - vertex1;
            Vector3 pointOnEdge = vertex1 + Vector3.Project(targetPoint - vertex1, edgeDirection);

            // Calculate the distance between the target point and the closest point on the edge
            float distance = Vector3.Distance(targetPoint, pointOnEdge);

            // Update the closest point if this distance is smaller
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = pointOnEdge;
            }
        }

        return closestPoint;
    }
}