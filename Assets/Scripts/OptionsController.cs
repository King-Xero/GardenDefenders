using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public Slider MusicVolumerSlider;
    public Slider SfxVolumeSlider;

    private MusicManager musicManager;
    private SFXManager sfxManager;

    // Use this for initialization
    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        sfxManager = FindObjectOfType<SFXManager>();

        MusicVolumerSlider.value = PlayerPrefsManager.GetMusicVolume();
        SfxVolumeSlider.value = PlayerPrefsManager.GetSFXVolume();
    }

    // Update is called once per frame
    void Update()
    {
        musicManager.SetVolume(MusicVolumerSlider.value);
        sfxManager.SetVolume(SfxVolumeSlider.value);
    }

    public void ShowOptionsMenu()
    {
        gameObject.SetActive(true);
    }

    public void SaveAndExit()
    {
        PlayerPrefsManager.SetMusicVolume(MusicVolumerSlider.value);
        PlayerPrefsManager.SetSFXVolume(SfxVolumeSlider.value);

        gameObject.SetActive(false);
    }

    public void SetDefaults()
    {
        MusicVolumerSlider.value = 0.8f;
        SfxVolumeSlider.value = 0.8f;
    }
}
