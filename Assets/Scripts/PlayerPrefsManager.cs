using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour
{
    private const string MUSIC_VOLUME_KEY = "music_volume";
    private const string SFX_VOLUME_KEY = "sfx_volume";
    private const string DIFFICULTY_KEY = "difficulty";
    private const string LEVEL_UNLOCKED_KEY = "level_unlocked_";
    private const string LANGUAGE_KEY = "language";
    private const string SELECTED_LEVEL_KEY = "selected_level";
    private const string LEVEL_COMPLETE_KEY = "level_complete_";
    

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
    }

    public static void SetMusicVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Music volume out of range");
        }
    }

    public static float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
    }

    public static void SetSFXVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Sfx volume out of range");
        }
    }

    public static void SetDifficulty(float difficulty)
    {
        if (difficulty >= 1f && difficulty <= 3f)
        {
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
        }
        else
        {
            Debug.LogError("Difficulty out of range");
        }
    }

    public static float GetDifficulty()
    {
        return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    }

    public static void UnlockLevel(int level)
    {
        //if (level <= SceneManager.sceneCountInBuildSettings -1)
        //{
            PlayerPrefs.SetInt(LEVEL_UNLOCKED_KEY + level, 1);
        //}
        //else
        //{
        //    Debug.LogError("Trying to unlock level not in the build order");
        //}
    }

    public static bool CheckLevelUnlocked(int level)
    {
        //if (level <= SceneManager.sceneCountInBuildSettings - 1)
        //{
            if (PlayerPrefs.GetInt(LEVEL_UNLOCKED_KEY + level) == 1)
            {
                return true;
            }
            return false;
        //}
        //Debug.LogError("Trying to check level not in the build order");
        //return false;
    }

    public static void CompleteLevel(int level)
    {
        PlayerPrefs.SetInt(LEVEL_COMPLETE_KEY, 1);
    }

    public static bool CheckLevelCompleted(int level)
    {
        if (PlayerPrefs.GetInt(LEVEL_COMPLETE_KEY) == level)
        {
            return true;
        }
        return false;
    }

    public static void SetLanguage(string language)
    {
        PlayerPrefs.SetString(LANGUAGE_KEY, language);
    }

    public static string GetLanguage()
    {
        return PlayerPrefs.GetString(LANGUAGE_KEY);
    }

    public static void SetSelectedLevel(int level)
    {
        PlayerPrefs.SetInt(SELECTED_LEVEL_KEY, level);
    }

    public static int GetSelectedLevel()
    {
        return PlayerPrefs.GetInt(SELECTED_LEVEL_KEY, 1);
    }
}
