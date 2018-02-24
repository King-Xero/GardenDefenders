using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    private const int NUMBEROFLANES = 5;

    public float SpawnDelay;
    //public GameObject[] AttackerPrefabs;
    public GameObject[] AttackerPools;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Time.timeSinceLevelLoad >= SpawnDelay)
        {
            //foreach (GameObject attackerPrefab in AttackerPrefabs)
            //{
            //    if (IsTimeToSpawn(attackerPrefab))
            //    {
            //        Spawn(attackerPrefab);
            //    }
            //}

            foreach (var pool in AttackerPools)
            {
                ObjectPooler attackerPool = pool.GetComponent<ObjectPooler>();
                var pooledObject = attackerPool.PoolObjectType;
                if (IsTimeToSpawn(pooledObject))
                {
                    Spawn(attackerPool.GetPooledObject());
                }
            }
        }
    }

    private bool IsTimeToSpawn(GameObject attackerPrefab)
    {
        Attacker attacker = attackerPrefab.GetComponent<Attacker>();

        if (!attacker)
        {
            return false;
        }

        float meanSpawnDelay = attacker.SpawnTime;
        float spawnsPerSecond = 1 / meanSpawnDelay;

        if (Time.deltaTime > meanSpawnDelay)
        {
            Debug.LogWarning("Spawn rate is capped by frame rate");
        }

        float spawnThreshold = spawnsPerSecond * Time.deltaTime / NUMBEROFLANES;

        return Random.value < spawnThreshold;
    }

    private void Spawn(GameObject attacker)
    {
        //Instantiate(attacker, transform.position, Quaternion.identity, transform);

        attacker.transform.position = transform.position;
        attacker.SetActive(true);
    }
}