using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIOverlayManager : MonoBehaviour
{
    public PopUpText WaveMessage;
    public string WaveKey;
    public LocalizedText LevelEndPanelText;
    public string CompleteKey, FailKey;
    public GameObject LevelEndPanel, NextLevelButton, RetryButton;

    private const float POPUP_DISPLAY_TIME = 3f;
    private SFXManager sfxManager;
    private LevelSceneManager levelSceneManager;
    private int currentLevel;

    void Awake()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        if (!sfxManager) Debug.LogError("SFX Manager not found");
        levelSceneManager = FindObjectOfType<LevelSceneManager>();
        if (!levelSceneManager) Debug.LogError("Level Scene Manager not found");
    }

    void Start ()
    {
	    if (!LevelEndPanel) Debug.LogError("Level end panel not found");
        if (!LevelEndPanel) Debug.LogError("Level end panel not found");
    }

    public void ShowWaveMessage(int numWave)
    {
        sfxManager.PlayClip(sfxManager.WaveStarted);
        WaveMessage.Text = LocalizationManager.Instance.GetLocalizedValue(WaveKey) + ": " + numWave;
        WaveMessage.DisplayText(POPUP_DISPLAY_TIME);
    }

    public void ShowPanel(LevelEndCondition endCondition, int level)
    {
        currentLevel = level;

        ShowPanel(endCondition);
    }

    public void ShowPanel(LevelEndCondition endCondition)
    {
        LevelSceneManager.GameIsActive = false;

        int maxLevel = LevelEditorDataManager.Instance.GetLevelsDictionary().Last().Key;

        switch (endCondition)
        {
            case LevelEndCondition.LevelComplete:
                LevelEndPanelText.key = CompleteKey;
                if (currentLevel != maxLevel)
                {
                    NextLevelButton.SetActive(true);
                }
                sfxManager.PlayClip(sfxManager.LevelEnd);
                break;
            case LevelEndCondition.LevelFailed:
                LevelEndPanelText.key = FailKey;
                RetryButton.SetActive(true);
                //sfxManager.PlayClip(sfxManager.LevelEnd);
                break;
        }
        LevelEndPanel.SetActive(true);
    }

    public void StartNextLevel()
    {
        PlayerPrefsManager.SetSelectedLevel(currentLevel + 1);
        levelSceneManager.LoadGameScene();
    }

    public void RetryCurrentLevel()
    {
        levelSceneManager.LoadGameScene();
    }
}

public enum LevelEndCondition
{
    LevelComplete,
    LevelFailed
}