using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour {

    public static LocalizationManager Instance;

    public string DefaultLanguageFile;
    public event EventHandler LanguageUpdateEventHandler = delegate { };

    private const string MISSING_STRING_TEXT = "No localization found";
    private Dictionary<string, string> localizedTextDictionary;
    

    private void Awake()
    {
        SetLangauge();
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void SetLangauge()
    {
        string lang = PlayerPrefsManager.GetLanguage();

        if (String.IsNullOrEmpty(lang))
        {
            lang = DefaultLanguageFile;
        }
        LoadLocalizedText(lang);
    }

    public void LoadLocalizedText(string filename)
    {
        localizedTextDictionary = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, filename);
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(jsonString);

            for (int i = 0; i < loadedData.Items.Length; i++)
            {
                localizedTextDictionary.Add(loadedData.Items[i].key, loadedData.Items[i].value);
            }

        } else
        {
            Debug.LogError("Language file not found.");
        }
        LanguageUpdateEventHandler(this, EventArgs.Empty);
    }

    public string GetLocalizedValue(string key)
    {
        string ret;
        if(!localizedTextDictionary.TryGetValue(key, out ret))
        {
            ret = MISSING_STRING_TEXT;
        }
        return ret;
    }
}
