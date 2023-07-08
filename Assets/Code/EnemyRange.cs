using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    private readonly List<Transform> targets = new();
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
        return transform.GetClosestTarget(targets);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        targets.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        targets.Remove(other.transform);
    }
}