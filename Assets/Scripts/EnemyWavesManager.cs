﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyWavesManager : MonoBehaviour
{
    //Higher the number, slower the spawn rate
    //ToDo Use this number to create difficulty settings
    private const int NUMBER_OF_LANES = 1; //5;
    private const float WAVE_SPAWN_DELAY = 0f;

    private int numTotalWaves, numEnemiesInWave, numTotalEnemiesInLevel, numCurrentWave, numCurrentLevel;
    private Level currentLevel;
    private Wave currentWave;
    private Dictionary<GameObject, int> currentWaveDictionary;
    private WaveSlider waveSlider;
    
    public UIOverlayManager UIOverlay;
    public List<GameObject> SpawnPoints;
    public List<ObjectPooler> EnemyPools;
    
    
    void Awake ()
	{
	    if (SpawnPoints == null)
	    {
	        Debug.LogWarning("No enemy spawn points found");
	    }

        numCurrentLevel = PlayerPrefsManager.GetSelectedLevel();
        currentLevel = LevelEditorDataManager.Instance.GetLevelData(numCurrentLevel);
        numCurrentWave = 0;
	    numTotalEnemiesInLevel = 0;
	    numTotalWaves = currentLevel.Waves.Length;
	    numEnemiesInWave = 0;
	    
	    waveSlider = FindObjectOfType<WaveSlider>();

        SetupLevelObjectPools();
	}
	
	void Update () {
	    if (numEnemiesInWave <= 0)
	    {
	        if (numCurrentWave < numTotalWaves)
	        {
	            numCurrentWave++;
	            Invoke("StartNextWave", WAVE_SPAWN_DELAY);
	        }
	        else
	        {
	            LevelComplete();
	        }
	    }
	    else if (numEnemiesInWave > 0 && numCurrentWave <= numTotalWaves)
	    {
	        UpdateWave();
	    }
	}

    void SetupLevelObjectPools()
    {
        EnemyPools = new List<ObjectPooler>();
        var levelEnemiesDictionary = new Dictionary<GameObject, int>();

        foreach (var wave in currentLevel.Waves)
        {
            foreach (var enemyType in wave.WaveEnemies)
            {
                if (!levelEnemiesDictionary.ContainsKey(enemyType.EnemyPrefab))
                {
                    levelEnemiesDictionary.Add(enemyType.EnemyPrefab, enemyType.Spawns);
                    numTotalEnemiesInLevel += enemyType.Spawns;
                }
                else
                {
                    levelEnemiesDictionary[enemyType.EnemyPrefab] += enemyType.Spawns;
                    numTotalEnemiesInLevel += enemyType.Spawns;
                }
            }
        }

        waveSlider.SetTotalEnemiesInLevel(numTotalEnemiesInLevel);

        foreach (var enemyType in levelEnemiesDictionary)
        {
            var enemyPool = new GameObject(enemyType.Key.name + " Object Pool").AddComponent<ObjectPooler>();
            enemyPool.transform.SetParent(transform);
            enemyPool.PoolObjectType = enemyType.Key;
            enemyPool.PooledAmount = Mathf.RoundToInt(enemyType.Value * 0.7f);
            if (enemyPool.PooledAmount < enemyType.Value)
            {
                enemyPool.CanGrow = true;
            }
            //Hack to reuse the object pooler for attackers and defenders.
            enemyPool.PooledObjectPrefabs = new []{enemyType.Key};
            
            EnemyPools.Add(enemyPool);
        }
    }

    void SpawnWave()
    {
        currentWave = currentLevel.Waves[numCurrentWave - 1];
        currentWaveDictionary = new Dictionary<GameObject, int>();

        foreach (var enemyType in currentWave.WaveEnemies)
        {
            numEnemiesInWave += enemyType.Spawns;
            currentWaveDictionary.Add(enemyType.EnemyPrefab, enemyType.Spawns);
        }
    }

    void UpdateWave()
    {
        List<Action> dictionaryActions = new List<Action>();

        foreach (var enemy in currentWaveDictionary.Keys)
        {
            if (!(currentWaveDictionary[enemy] <= 0) && IsTimeToSpawn(enemy))
            {
                SpawnEnemy(enemy);
                dictionaryActions.Add(() =>
                {
                    currentWaveDictionary[enemy] -= 1;
                });
            }
        }
        foreach (var action in dictionaryActions)
        {
            action();
        }

    }

    bool IsTimeToSpawn(GameObject enemyGameObject)
    {
        Attacker attacker = enemyGameObject.GetComponent<Attacker>();

        if (attacker == null)
        {
            return false;
        }

        float meanSpawnDelay = attacker.SpawnTime;
        float spawnsPerSecond = 1 / meanSpawnDelay;

        if (Time.deltaTime > meanSpawnDelay)
        {
            Debug.LogWarning("Spawn rate is capped by frame rate");
        }

        float spawnThreshold = spawnsPerSecond * Time.deltaTime / NUMBER_OF_LANES;

        return Random.value < spawnThreshold;
    }

    void SpawnEnemy(GameObject enemyGameObject)
    {
        GameObject spawnPoint = SpawnPoints.ElementAt(Mathf.RoundToInt(Random.Range(0, SpawnPoints.Count)));
        ObjectPooler enemyObjectPooler = EnemyPools.Find(p => p.PoolObjectType == enemyGameObject);
        GameObject enemyToSpawn = enemyObjectPooler.GetPooledObject();
        enemyToSpawn.transform.position = spawnPoint.transform.position;
        enemyToSpawn.SetActive(true);
    }

    void LevelComplete()
    {
        //ToDo Disable normal functionality
        //PlayerPrefsManager.CompleteLevel(numCurrentLevel);
        //PlayerPrefsManager.UnlockLevel(numCurrentLevel + 1);
        Debug.Log("Level Finished");
        UIOverlay.ShowPanel(LevelEndCondition.LevelComplete, numCurrentLevel);
    }

    void StartNextWave()
    {
        UIOverlay.ShowWaveMessage(numCurrentWave);
        SpawnWave();
    }
    
    public int GetCurrentWave()
    {
        return numCurrentWave;
    }

    public void EnemyDestroyed(GameObject enemyGameObject)
    {
        Attacker destroyedattacker = enemyGameObject.GetComponent<Attacker>();

        if (destroyedattacker != null)
        {
            foreach (var enemy in currentWaveDictionary.Keys)
            {
                var dictAttacker = enemy.GetComponent<Attacker>();
                if (dictAttacker != null && dictAttacker.AttackerId == destroyedattacker.AttackerId)
                {
                    numEnemiesInWave--;
                    waveSlider.UpdateSlider();
                    return;
                }
            }
        }
    }
}
