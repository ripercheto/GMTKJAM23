using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformUtility
{
    public static Vector3 GetFlatPosition(this Vector3 position)
    {
        position.y = 0;
        return position;
    }

    public static Vector3 To3D(this Vector2 position)
    {
        return new Vector3(position.x, 0, position.y);
    }

    public static Vector3 GetFlatPosition(this Transform transform)
    {
        return GetFlatPosition(transform.position);
    }

    public static Transform GetClosestTarget(this Transform transform, IEnumerable<Transform> targets)
    {
        var pos = transform.position;
        var closestDistance = float.MaxValue;
        Transform closestTarget = null;
        foreach (var target in targets)
        {
            if (target == null)
            {
                continue;
            }
            var distance = (target.position - pos).sqrMagnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }
        return closestTarget;
    }

    public static Transform GetClosestTarget(this Transform transform, IEnumerable<MonoBehaviour> targets)
    {
        var pos = transform.position;
        var closestDistance = float.MaxValue;
        Transform closestTarget = null;
        foreach (var target in targets)
        {
            if (target == null)
            {
                continue;
            }
            var distance = (target.transform.position - pos).sqrMagnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target.transform;
            }
        }
        return closestTarget;
    }
}