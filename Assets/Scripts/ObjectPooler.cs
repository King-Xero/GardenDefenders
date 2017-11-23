using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    
    public int PooledAmount;
    public bool CanGrow;

    public GameObject PoolObjectType;

    public GameObject[] PooledObjectPrefabs;

    private List<GameObject> pooledObjects;

	void Start ()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < PooledAmount; i++)
        {
            GameObject obj = CreatePoolObject();
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (CanGrow)
        {
            GameObject obj = CreatePoolObject();
            pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }

    private GameObject CreatePoolObject()
    {
        return Instantiate(PooledObjectPrefabs.ElementAt(Mathf.RoundToInt(Random.Range(0, PooledObjectPrefabs.Count()))), transform.position, Quaternion.identity, transform) as GameObject;
    }
}
