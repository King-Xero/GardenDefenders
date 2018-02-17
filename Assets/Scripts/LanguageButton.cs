using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageButton : MonoBehaviour
{
    private LocalizationManager localizationManager;
    private LevelSceneManager levelSceneManager;

    public string LanguageFileName;

    void Awake()
    {
        localizationManager = FindObjectOfType<LocalizationManager>();
        levelSceneManager = FindObjectOfType<LevelSceneManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonClicked()
    {
        PlayerPrefsManager.SetLanguage(LanguageFileName);
        localizationManager.LoadLocalizedText(LanguageFileName);
        levelSceneManager.LoadMainMenuScene();
    }
}
