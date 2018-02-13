using System;
using UnityEngine;

[Serializable]
public class WaveEnemy
{
    [HideInInspector]
    public string Name;
    [Tooltip("Enemy prefab")]
    public GameObject EnemyPrefab;
    [Tooltip("Number of enemies of this type to spawn during the wave.")]
    public int Spawns;
}