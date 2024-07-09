using System.Collections.Generic;
using UnityEngine;

public class Pool : Singleton<Pool>
{
    public int amountToPool;
    public GameObject prefab;

    private List<GameObject> pooledObjects;

    protected void Awake()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            AddSlot();
        }
    }

    private void AddSlot()
    {
        var go = Instantiate(prefab, transform.position, Quaternion.identity);
        go.transform.SetParent(transform);
        go.gameObject.SetActive(false);
        pooledObjects.Add(go);
    }

    public GameObject GetPooledObjects()
    {
        foreach (var pooledObject in pooledObjects)
        {
            if (!pooledObject.gameObject.activeInHierarchy)
            {
                return pooledObject;
            }
        }
        AddSlot();
        return pooledObjects[^1];
    }
}
