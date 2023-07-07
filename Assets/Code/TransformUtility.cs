using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformUtility
{
    public static Vector3 GetFlatPosition(this Transform transform)
    {
        var pos = transform.position;
        pos.y = 0;
        return pos;
    }
}
