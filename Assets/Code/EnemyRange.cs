using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public float range = 2;

    public bool HasTargetsInRange
    {
        get
        {

            var target = GetClosestTarget();
            if (target == null)
            {
                return false;
            }
            if (Vector3.Distance(target.position, transform.position) < range)
            {
                return true;
            }
            return false;

        }
    }

    public Transform GetClosestTarget()
    {
        return transform.GetClosestTarget(MainCharacters.targets);
    }
}