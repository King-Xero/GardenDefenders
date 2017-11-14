using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartVolume : MonoBehaviour
{
    private MusicManager musicManager;
    private SFXManager sfxManager;

    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        sfxManager = FindObjectOfType<SFXManager>();

        if (musicManager)
        {
            musicManager.SetVolume(PlayerPrefsManager.GetMusicVolume());
        }
        else
        {
            Debug.LogWarning("No music manager found");
        }
        if (sfxManager)
        {
            sfxManager.SetVolume(PlayerPrefsManager.GetSFXVolume());
        }
        else
        {
            Debug.LogWarning("No sfx manager found");
        }
    }
}