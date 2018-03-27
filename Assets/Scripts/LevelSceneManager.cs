using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneManager : MonoBehaviour
{
    public static bool GameIsPaused, GameIsActive;

    public bool AutoLoadNextlevel;
    public float AutoLoadAfter;
    public int LanguageScene;
    public int MainMenuScene;
    public int GameScene;
    public int WinScene;
    public int LoseScene;

    void Start()
    {
        if (AutoLoadNextlevel)
        {
            Invoke("AutoLoadNextScene", AutoLoadAfter);
        }
        else
        {
            Debug.Log("Level auto load disabled");
        }
    }

    void Update()
    {
        if (!GameIsActive)
        {
            Time.timeScale = 0.0f;
        }
    }

    public void LoadScene(int scene)
    {
        if (GameIsPaused)
        {
            GameIsPaused = false;
            Time.timeScale = 1.0f;
        }
        if (!GameIsActive)
        {
            GameIsActive = true;
        }

        SceneManager.LoadScene(scene);
    }

    //Only used after splash screen
    private void AutoLoadNextScene()
    {
        string lang = PlayerPrefsManager.GetLanguage();

        if (String.IsNullOrEmpty(lang))
        {
            LoadScene(LanguageScene);
        }
        else
        {
            LoadScene(MainMenuScene);
        }
    }

    public void LoadMainMenuScene()
    {
        LoadScene(MainMenuScene);
    }

    public void LoadLoseScene()
    {
        LoadScene(LoseScene);
    }

    public void LoadGameScene()
    {
        LoadScene(GameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
