using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPool<T> : MonoBehaviour where T : MonoBehaviour
{
    public T prefab;

    private Queue<Vector3> spawnQueue = new Queue<Vector3>();
    private List<T> activeObjects = new List<T>();
    private Queue<T> inactiveObjects = new Queue<T>();

    public void Activate(Vector3 position)
    {
        spawnQueue.Enqueue(position);
    }

    public void Deactivate(T obj)
    {
        obj.gameObject.SetActive(false);
        inactiveObjects.Enqueue(obj);
    }

    private void Update()
    {
        if (Time.frameCount % 2 != 0)
        {
            return;
        }

        if (spawnQueue.Count == 0)
        {
            return;
        }

        var pos = spawnQueue.Dequeue();

        if (inactiveObjects.Count == 0)
        {
            var newObject = Instantiate(prefab, pos, Quaternion.identity);
            activeObjects.Add(newObject);
        }
        else
        {
            var newObject = inactiveObjects.Dequeue();
            newObject.transform.position = pos;
            newObject.gameObject.SetActive(true);
        }
    }
}