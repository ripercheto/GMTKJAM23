using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPool<T> : MonoBehaviour where T : Component
{
    public T prefab;
    public int framesBetweenSpawns = 1;

    private Queue<(Vector3 pos, Action<T> action)> spawnQueue = new Queue<(Vector3, Action<T>)>();
    private List<T> activeObjects = new List<T>();
    private Queue<T> inactiveObjects = new Queue<T>();

    public void Activate(Vector3 position, Action<T> action = null)
    {
        spawnQueue.Enqueue((position, action));
    }

    public void Deactivate(T obj)
    {
        obj.gameObject.SetActive(false);
        inactiveObjects.Enqueue(obj);
    }

    private void Update()
    {
        if (Time.frameCount % (framesBetweenSpawns + 1) != 0)
        {
            return;
        }

        if (spawnQueue.Count == 0)
        {
            return;
        }

        var data = spawnQueue.Dequeue();

        if (inactiveObjects.Count == 0)
        {
            var newObject = Instantiate(prefab, data.pos, Quaternion.identity);
#if UNITY_EDITOR
            newObject.name += Time.frameCount;
#endif
            activeObjects.Add(newObject);
            data.action?.Invoke(newObject);

        }
        else
        {
            var newObject = inactiveObjects.Dequeue();
            activeObjects.Add(newObject);
            newObject.transform.position = data.pos;
            newObject.gameObject.SetActive(true);
            data.action?.Invoke(newObject);
        }
    }
}