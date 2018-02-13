using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LevelEditor : EditorWindow
{
    public LevelData GameLevelData;

    [MenuItem("Window/Level Editor")]
    static void Init()
    {
        EditorWindow.GetWindow<LevelEditor>("Level Editor");
    }

    void OnGUI()
    {
        GUILayout.Label("Levels", EditorStyles.boldLabel);
        
        if (GameLevelData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("GameLevelData");
            EditorGUILayout.PropertyField(serializedProperty, true);

            SetupInspectorNames();

            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Save level data"))
            {
                SaveLevelData();
            }
        }

        if (GUILayout.Button("Load level data"))
        {
            LoadLevelData();
        }

        if (GUILayout.Button("Create new level data"))
        {
            CreateNewLevelData();
        }
    }

    private void SetupInspectorNames()
    {
        var levels = GameLevelData.Levels;
        if (levels != null)
        {
            foreach (var level in levels)
            {
                level.Name = "Level " + (ArrayUtility.IndexOf(levels, level) + 1);

                var waves = level.Waves;

                if (waves != null)
                {
                    foreach (var wave in waves)
                    {
                        wave.Name = "Wave " + (ArrayUtility.IndexOf(waves, wave) + 1);

                        var enemies = wave.WaveEnemies;

                        if (enemies != null)
                        {
                            foreach (var enemy in enemies)
                            {
                                enemy.Name = "Enemy " + (ArrayUtility.IndexOf(enemies, enemy) + 1);
                            }
                        }
                    }
                }
            }
        }
    }

    private void LoadLevelData()
    {
        string filePath = EditorUtility.OpenFilePanel("Select level data file", Application.streamingAssetsPath, "gxg");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);

            GameLevelData = JsonUtility.FromJson<LevelData>(dataAsJson);
        }
    }

    private void SaveLevelData()
    {
        string filePath = EditorUtility.SaveFilePanel("Save level data file", Application.streamingAssetsPath, "", "gxg");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(GameLevelData);
            File.WriteAllText(filePath, dataAsJson);
        }
    }

    private void CreateNewLevelData()
    {
        GameLevelData = new LevelData();
    }
}
