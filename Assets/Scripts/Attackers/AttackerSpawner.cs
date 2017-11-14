using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    private const int NUMBEROFLANES = 5;

    public float SpawnDelay;
    public GameObject[] AttackerPrefabs;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= SpawnDelay)
        {
            foreach (GameObject attackerPrefab in AttackerPrefabs)
            {
                if (isTimeToSpawn(attackerPrefab))
                {
                    Spawn(attackerPrefab);
                }
            }
        }
    }

    private bool isTimeToSpawn(GameObject attackerPrefab)
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

    private void Spawn(GameObject attackerPrefab)
    {
        Instantiate(attackerPrefab, transform.position, Quaternion.identity, transform);
    }
}