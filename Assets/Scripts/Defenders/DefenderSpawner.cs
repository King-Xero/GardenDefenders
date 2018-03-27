using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DefenderSpawner : MonoBehaviour
{
    private Camera gameCamera;
    private GameObject defenderParent;
    private StarsCounter starsCounter;
    private SFXManager sfxManager;
    
    
    void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        gameCamera = FindObjectOfType<Camera>();
        starsCounter = FindObjectOfType<StarsCounter>();

        defenderParent = GameObject.Find("Defenders");
        if (!defenderParent)
        {
            defenderParent = new GameObject("Defenders");
        }
    }

    
    void Update()
    {
    }

    private void OnMouseDown()
    {
        Debug.Log("Attempting to spawn a defender");
        if (LevelSceneManager.GameIsActive && !LevelSceneManager.GameIsPaused && !DefenderButton.ShovelActive &&
            DefenderButton.SelectedDefenderPrefabs != null && DefenderButton.SelectedDefenderPrefabs.Any())
        {
            Defender selectedDefender = DefenderButton.SelectedDefenderPrefabs.ElementAt(Mathf.RoundToInt(Random.Range(0, DefenderButton.SelectedDefenderPrefabs.Count()))).GetComponent<Defender>();
            if (starsCounter.UseStars(selectedDefender.SpawnCost) == StarsCounter.Status.Success)
            {
                if (sfxManager)
                {
                    sfxManager.PlayClip(sfxManager.DefenderPlaced);
                }
                Instantiate(selectedDefender, SnapToGrid(WorldPointOfMouseClick()), Quaternion.identity, defenderParent.transform);
            }
            else
            {
                starsCounter.TriggerFailAnimation();
            }
        }
    }

    private Vector2 WorldPointOfMouseClick()
    {
        var worldPoint = gameCamera.ScreenToWorldPoint(Input.mousePosition);

        return new Vector2(worldPoint.x, worldPoint.y);
    }

    private Vector2 SnapToGrid(Vector2 rawPoint)
    {
        var x = Mathf.Round(rawPoint.x);
        x = Mathf.Clamp(x, 1, 9);

        var y = Mathf.Round(rawPoint.y);
        y = Mathf.Clamp(y, 1, 5);

        return new Vector2(x, y);
    }
}