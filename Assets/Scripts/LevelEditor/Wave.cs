using System;
using UnityEngine;

[Serializable]
public class Wave
{
    [HideInInspector]
    public string Name;
    public WaveEnemy[] WaveEnemies;
}