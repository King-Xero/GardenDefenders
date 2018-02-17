using UnityEngine;
using UnityEngine.UI;

public class WaveSlider : MonoBehaviour
{
    public GameObject WinLabel;
    public Text levelText, waveText;

    private Slider waveSlider;
    private LevelSceneManager levelSceneManager;
    private bool isEndOfLevel;
    private SFXManager sfxManager;
    private EnemyWavesManager enemyWavesManager;
    private string levelLocalizedString, waveLocalizedString;
    private float totalEnemiesInLevel;
    private int numCurrentLevel;

    private void Start()
    {
        waveSlider = GetComponent<Slider>();
        sfxManager = FindObjectOfType<SFXManager>();
        levelSceneManager = FindObjectOfType<LevelSceneManager>();
        enemyWavesManager = FindObjectOfType<EnemyWavesManager>();
        numCurrentLevel = PlayerPrefsManager.GetSelectedLevel();

        if (!WinLabel)
        {
            Debug.LogWarning("No win label found");
        }
        WinLabel.SetActive(false);

        if (!levelText)
        {
            Debug.Log("Level text not found");
        }
        else
        {
            levelLocalizedString = levelText.text;
        }

        if (!waveText)
        {
            Debug.Log("Wave text not found");
        }
        else
        {
            waveLocalizedString = waveText.text;
        }
    }

    private void Update()
    {
        levelText.text = levelLocalizedString + ": " + numCurrentLevel;
        waveText.text = waveLocalizedString + ": " + enemyWavesManager.GetCurrentWave();
    }

    //Called once at the start of a level
    public void SetTotalEnemiesInLevel(int total)
    {
        totalEnemiesInLevel = total;
    }

    //Called every time an enemy is killed
    public void UpdateSlider()
    {
        waveSlider.value -= (1 / totalEnemiesInLevel);
    }
}