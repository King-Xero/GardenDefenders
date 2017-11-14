using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerSlider : MonoBehaviour
{
    [Tooltip("Level duration in seconds")]
    public float LevelDuration;
    public GameObject WinLabel;

    private Slider timerSlider;
    private LevelManager levelManager;
    private bool isEndOfLevel;
    private SFXManager sfxManager;


    //Spawndelay and end spawning at end of timer.

    // Use this for initialization
    void Start()
    {
        timerSlider = GetComponent<Slider>();

        sfxManager = FindObjectOfType<SFXManager>();
        levelManager = FindObjectOfType<LevelManager>();
        FindWinLabel();
    }

    private void FindWinLabel()
    {
        if (!WinLabel)
        {
            Debug.LogWarning("No win label found");
        }
        WinLabel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timerSlider.value = 1 - (Time.timeSinceLevelLoad / LevelDuration);

        if (Time.timeSinceLevelLoad >= LevelDuration && !isEndOfLevel)
        {
            print("Level over");
            if (sfxManager)
            {
                sfxManager.PlayClip(sfxManager.LevelEnd);
            }
            WinLabel.SetActive(true);
            isEndOfLevel = true;
            Invoke("LoadNextLevel", sfxManager.LevelEnd.length);
        }
    }

    void LoadNextLevel()
    {
        Debug.Log("Loading next level");
        levelManager.LoadNextLevel();
    }
}