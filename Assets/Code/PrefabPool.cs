using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPool<T> : MonoBehaviour where T : MonoBehaviour
{
    public T prefab;
    public int framesBetweenSpawns = 1;
    
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
        if (Time.frameCount % (framesBetweenSpawns + 1) != 0)
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
#if UNITY_EDITOR
            newObject.name += Time.frameCount;
#endif
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