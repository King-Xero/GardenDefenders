using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameLevelSelect : MonoBehaviour
{
    private Dictionary<int, Level> levelsDictionary;
    private int selectedLevel, maxUnlockedLevel, maxLevel, maxCompleteLevel;
    private Text levelButtonText;
    private Image levelButtonImage, levelStarImage;
    private LevelSceneManager levelSceneManager;

    public Button LevelButton, PreviousButton, NextButton;
    public Sprite UnlockedLevelSprite;
    public Sprite LockedLevelSprite;
    public Sprite CompletedLevelSprite;
    public Sprite UncompletedLevelSprite;

    
    void Start ()
	{
	    levelsDictionary = LevelEditorDataManager.Instance.GetLevelsDictionary();
	    foreach (var levelEntry in levelsDictionary)
	    {
	        var level = levelEntry.Key;
	        if (PlayerPrefsManager.CheckLevelUnlocked(level))
	        {
	            maxUnlockedLevel = level;
	        }
	        if (PlayerPrefsManager.CheckLevelCompleted(level))
	        {
	            maxCompleteLevel = level;
	        }
	        maxLevel = level;
	    }
	    selectedLevel = maxUnlockedLevel;

	    levelButtonImage = LevelButton.GetComponent<Image>();
	    levelStarImage = LevelButton.GetComponentsInChildren<Image>().Single(star => star.transform.parent == LevelButton.transform);
        levelButtonText = LevelButton.GetComponentInChildren<Text>();

	    levelSceneManager = FindObjectOfType<LevelSceneManager>();

	    UpdateLevelSelectionText();
	    UpdateLevelButton();
	    UpdatePreviousButton();
	    UpdateNextButton();
    }
	
	
	void Update ()
    {
        UpdateLevelSelectionText();
        UpdateLevelButton();
        UpdatePreviousButton();
        UpdateNextButton();
    }

    private void UpdateNextButton()
    {
        if (selectedLevel == maxLevel)
        {
            NextButton.gameObject.SetActive(false);
        }
        else
        {
            NextButton.gameObject.SetActive(true);
        }
    }

    private void UpdatePreviousButton()
    {
        if (selectedLevel == 1)
        {
            PreviousButton.gameObject.SetActive(false);
        }
        else
        {
            PreviousButton.gameObject.SetActive(true);
        }
    }

    private void UpdateLevelButtonStar()
    {
        if (selectedLevel > maxCompleteLevel && selectedLevel <= maxUnlockedLevel)
        {
            levelStarImage.gameObject.SetActive(true);
            levelStarImage.sprite = UncompletedLevelSprite;
        }
        else if (selectedLevel == maxCompleteLevel && selectedLevel <= maxUnlockedLevel)
        {
            levelStarImage.gameObject.SetActive(true);
            levelStarImage.sprite = CompletedLevelSprite;
        }
        else if (selectedLevel > maxCompleteLevel && selectedLevel > maxUnlockedLevel)
        {
            levelStarImage.gameObject.SetActive(false);
        }
    }

    private void UpdateLevelSelectionText()
    {
        levelButtonText.text = selectedLevel.ToString("00");
    }

    private void UpdateLevelButton()
    {
        UpdateLevelButtonStar();

        if (selectedLevel <= maxUnlockedLevel)
        {
            LevelButton.enabled = true;
            levelButtonImage.sprite = UnlockedLevelSprite;
        }
        else
        {
            LevelButton.enabled = false;
            levelButtonImage.sprite = LockedLevelSprite;
        }
    }

    public void PreviousLevel()
    {
        if (selectedLevel == 1)
        {
            return;
        }
        selectedLevel--;
    }

    public void NextLevel()
    {
        if (selectedLevel == maxLevel)
        {
            return;
        }
        selectedLevel++;
    }

    public void SelectLevel()
    {
        PlayerPrefsManager.SetSelectedLevel(selectedLevel);
        levelSceneManager.LoadGameScene();
    }

    public void ShowLevelSelectMenu()
    {
        gameObject.SetActive(true);
    }

    public void ExitLevelSelectMenu()
    {
        gameObject.SetActive(false);
    }
}
