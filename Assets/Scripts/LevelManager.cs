using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool GameIsPaused;

    public bool AutoLoadNextlevel;
    public float AutoLoadNextLevelAfter;
    public int MainMenuScene;
    public int GameScene;
    public int WinScene;
    public int LoseScene;

    void Start()
    {
        if (AutoLoadNextlevel)
        {
            Invoke("LoadNextLevel", AutoLoadNextLevelAfter);
        }
        else
        {
            Debug.Log("Level auto load disabled");
        }
    }

    public void LoadScene(int scene)
    {
        if (GameIsPaused)
        {
            GameIsPaused = false;
            Time.timeScale = 1.0f;
        }

        SceneManager.LoadScene(scene);
    }

    public void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene >= SceneManager.sceneCountInBuildSettings)
        {
            LoadScene(WinScene);
        }
        else
        {
            LoadScene(nextScene);
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
