using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour {

    public Transform SpawnPoint;
    public GameObject StarPrefab;

    private void Start()
    {
        if (!SpawnPoint)
        {
            Debug.Log("No spawn point found");
        }
        if (!StarPrefab)
        {
            Debug.Log("No star prefab found");
        }
    }

    private void SpawnStar()
    {
        Instantiate(StarPrefab, SpawnPoint.position, Quaternion.identity, transform);
    }
}
