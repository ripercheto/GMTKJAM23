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
}
