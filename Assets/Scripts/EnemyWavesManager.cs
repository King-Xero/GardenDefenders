using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWavesManager : MonoBehaviour
{
    //Higher the number, slower the spawn rate
    //ToDo Use this number to crate difficulty settings
    private const int NUMBEROFLANES = 1;

    private int numTotalWaves, numEnemiesInWave, numTotalEnemiesInLevel, numCurrentWave;
    private Level currentLevel;
    private Wave currentWave;
    private Dictionary<GameObject, int> currentWaveDictionary;
    private WaveSlider waveSlider;
    
    public List<GameObject> SpawnPoints;
    public List<ObjectPooler> EnemyPools;

    public static int NumCurrentLevel = 1;

    // Use this for initialization
    void Awake ()
	{
	    if (SpawnPoints == null)
	    {
	        Debug.LogWarning("No enemy spawn points found");
	    }

	    currentLevel = LevelEditorDataManager.Instance.GetLevelData(1);
	    numCurrentWave = 0;
	    numTotalEnemiesInLevel = 0;
	    numTotalWaves = currentLevel.Waves.Length;
	    numEnemiesInWave = 0;

	    waveSlider = FindObjectOfType<WaveSlider>();

	    SetupLevelObjectPools();
	}
	
	// Update is called once per frame
	void Update () {
	    if (numEnemiesInWave <= 0)
	    {
	        if (numCurrentWave <= numTotalWaves)
	        {
	            numCurrentWave++;
                //Add wave spawn delay
                //Update UI for new wave
	            SpawnWave();
	        }
	        else
	        {
	            EndLevel();
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
        foreach (var enemy in currentWaveDictionary.Keys)
        {
            if (!(currentWaveDictionary[enemy] <= 0) && IsTimeToSpawn(enemy))
            {
                SpawnEnemy(enemy);
                currentWaveDictionary[enemy] -= 1;
            }
        }
    }

    bool IsTimeToSpawn(GameObject enemyGameObject)
    {
        Attacker attacker = enemyGameObject.GetComponent<Attacker>();

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

    void SpawnEnemy(GameObject enemyGameObject)
    {
        GameObject spawnPoint = SpawnPoints.ElementAt(Mathf.RoundToInt(Random.Range(0, SpawnPoints.Count)));
        ObjectPooler enemyObjectPooler = EnemyPools.Find(p => p.PoolObjectType == enemyGameObject);
        GameObject enemyToSpawn = enemyObjectPooler.GetPooledObject();
        enemyToSpawn.transform.position = spawnPoint.transform.position;
        enemyToSpawn.SetActive(true);
    }

    void EndLevel()
    {
        
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
