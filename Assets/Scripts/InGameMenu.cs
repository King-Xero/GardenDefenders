using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour {
    
    public GameObject MenuButton;

	
	void Start () {
        gameObject.SetActive(false);
	}
	
	
	void Update () {
		
	}

    public void ToggleMenu()
    {
        if (LevelSceneManager.GameIsActive)
        {
            if (LevelSceneManager.GameIsPaused)
            {
                gameObject.SetActive(false);
                //MenuButton.SetActive(true);
                Time.timeScale = 1.0f;
            }
            else
            {
                gameObject.SetActive(true);
                //MenuButton.SetActive(false);
                Time.timeScale = 0.0f;
            }
            LevelSceneManager.GameIsPaused = !LevelSceneManager.GameIsPaused;
        }
    }
}
