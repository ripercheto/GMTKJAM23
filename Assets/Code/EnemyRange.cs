using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public bool HasTargetsInRange
    {
        get
        {
            targets.RemoveAll(x => x == null);
            return targets.Count > 0;
        }
    }

    public Transform GetClosestTarget()
    {
        var closestDistance = float.MaxValue;
        Transform closestTarget = null;
        foreach (var target in targets)
        {
            var distance = (target.position - transform.position).sqrMagnitude;
            if (distance < closestDistance)
            {
                closestTarget = target;
            }
        }
        return closestTarget;
    }

    private readonly List<Transform> targets = new();

    private void OnTriggerEnter(Collider other)
    {
        targets.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        targets.Remove(other.transform);
    }
}