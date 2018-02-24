using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public Slider MusicVolumerSlider;
    public Slider SfxVolumeSlider;
    public LanguageSelector LanguageSelector;

    private MusicManager musicManager;
    private SFXManager sfxManager;

    
    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        sfxManager = FindObjectOfType<SFXManager>();

        MusicVolumerSlider.value = PlayerPrefsManager.GetMusicVolume();
        SfxVolumeSlider.value = PlayerPrefsManager.GetSFXVolume();
    }

    
    void Update()
    {
        if (musicManager)
        {
            musicManager.SetVolume(MusicVolumerSlider.value);
        }
        if (sfxManager)
        {
            sfxManager.SetVolume(SfxVolumeSlider.value);
        }
    }

    public void ShowOptionsMenu()
    {
        gameObject.SetActive(true);
    }

    public void SaveAndExit()
    {
        PlayerPrefsManager.SetMusicVolume(MusicVolumerSlider.value);
        PlayerPrefsManager.SetSFXVolume(SfxVolumeSlider.value);

        if (LanguageSelector)
        {
            PlayerPrefsManager.SetLanguage(LanguageSelector.CurrentSelection.FileName);
            LocalizationManager.Instance.LoadLocalizedText(LanguageSelector.CurrentSelection.FileName);
        }

        gameObject.SetActive(false);
    }

    public void SetDefaults()
    {
        MusicVolumerSlider.value = 0.8f;
        SfxVolumeSlider.value = 0.8f;
    }
}
