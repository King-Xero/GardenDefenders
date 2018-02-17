using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelEditorDataManager : MonoBehaviour
{
    public static LevelEditorDataManager Instance;

    public string DefaultLevelsFile;

    private Dictionary<int, Level> levelsDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        LoadLevelsData(DefaultLevelsFile);
    }

    public void LoadLevelsData(string filename)
    {
        levelsDictionary = new Dictionary<int, Level>();
        var filePath = Path.Combine(Application.streamingAssetsPath, "LevelData/" + filename);
        if (File.Exists(filePath))
        {
            var jsonString = File.ReadAllText(filePath);
            var loadedData = JsonUtility.FromJson<LevelData>(jsonString);

            for (var i = 0; i < loadedData.Levels.Length; i++)
                levelsDictionary.Add(i + 1, loadedData.Levels[i]);
        }
        else
        {
            Debug.LogError("Level file not found.");
        }
    }

    public Level GetLevelData(int levelNumber)
    {
        Level ret;
        if (!levelsDictionary.TryGetValue(levelNumber, out ret))
        {
            Debug.LogError("Level not found");
            ret = null;
        }
        return ret;
    }

    public Dictionary<int, Level> GetLevelsDictionary()
    {
        return levelsDictionary;
    }
}