using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;

public class MinMaxDrawer<T> : OdinValueDrawer<T> where T : MinMax
{
    private InspectorProperty min, max;

    protected override void Initialize()
    {
        min = Property.FindChild(x => x.Name == nameof(MinMaxFloat.min), false);
        max = Property.FindChild(x => x.Name == nameof(MinMaxFloat.max), false);
    }

    protected override void DrawPropertyLayout(GUIContent label)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(label);
            var lw = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 32;
            min.Draw();
            max.Draw();
            EditorGUIUtility.labelWidth = lw;
        }
        GUILayout.EndHorizontal();
    }
}
#endif

[InlineProperty]
public abstract class MinMax
{
}

[InlineProperty]
public abstract class MinMax<T> : MinMax
{
    public T min;
    public T max;

    public abstract T Lerp(float a);
    public abstract T Clamp(float a);
    public abstract T GetRandom();
}

[Serializable]
public class MinMaxFloat : MinMax<float>
{
    public override float Lerp(float a)
    {
        return Mathf.Lerp(min, max, a);
    }

    public override float Clamp(float a)
    {
        return Mathf.Clamp(a, min, max);
    }

    public override float GetRandom()
    {
        return Random.Range(min, max);
    }
}

[Serializable]
public class MinMaxInt : MinMax<int>
{
    public override int Lerp(float a)
    {
        return (int)Mathf.Lerp(min, max, a);
    }

    public override int Clamp(float a)
    {
        return Mathf.Clamp((int)a, min, max);
    }

    public override int GetRandom()
    {
        return Random.Range(min, max + 1);
    }
}