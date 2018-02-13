using System;
using UnityEngine;

[Serializable]
public class Level
{
    [HideInInspector]
    public string Name;
    public Wave[] Waves;
}