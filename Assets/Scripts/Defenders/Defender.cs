using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    public int SpawnCost;

    public GameObject Smuggler { get; set; }

    private StarsCounter starsCounter;
    private Shooter shooter;
    private SFXManager sfxManager;

    void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        starsCounter = FindObjectOfType<StarsCounter>();
        shooter = GetComponentInChildren<Shooter>();
    }

    void Update()
    {
        if (!Smuggler && shooter)
        {
            shooter.enabled = true;
        }
    }

    private void AddStars(int stars)
    {
        starsCounter.AddStars(stars);
    }

    public void Smuggle(GameObject attemptingSmuggler)
    {
        Smuggler = attemptingSmuggler;
        if (shooter)
        {
            shooter.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Defender Clicked");
        if (!LevelSceneManager.GameIsPaused && DefenderButton.ShovelActive)
        {
            if (sfxManager)
            {
                sfxManager.PlayClip(sfxManager.DefenderRemoved);
            }
            Destroy(gameObject);
        }
    }
}