using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DefenderButton : MonoBehaviour
{
    public static GameObject[] SelectedDefenderPrefabs;
    public static bool ShovelActive;
    public Sprite ButtonImage;
    public Sprite ButtonActiveImage;
    public GameObject[] DefenderPrefabs;
    public bool IsShovelButton;

    private DefenderButton[] defenderButtons;
    private Animator animator;
    private Image defenderImage;
    private Image buttonImage;
    private Text costText;
    
    
    void Start()
    {
        defenderButtons = FindObjectsOfType<DefenderButton>();

        buttonImage = GetComponentInChildren<Image>();

        if (!buttonImage)
        {
            Debug.LogWarning("Button image component not found");
        }

        if (IsShovelButton)
        {
            animator = GetComponent<Animator>();
        }
        else
        {
            defenderImage = GetComponentsInChildren<Image>().Single(img => img.transform.childCount == 0);

            if (!defenderImage)
            {
                Debug.LogWarning("Defender image component not found");
            }

            costText = GetComponentInChildren<Text>();

            if (costText)
            {
                costText.text = DefenderPrefabs.FirstOrDefault().GetComponent<Defender>().SpawnCost.ToString();
            } else
            {
                Debug.LogWarning("Text component not found");
            }

        }
    }

     
    void Update()
    {
        if (animator)
        {
            animator.SetBool("isActive", ShovelActive);
        }
    }

    private void OnMouseDown()
    {
        if (LevelSceneManager.GameIsActive && !LevelSceneManager.GameIsPaused)
        {
            foreach (DefenderButton defenderButton in defenderButtons)
            {
                defenderButton.GetComponentInChildren<Image>().sprite = defenderButton.ButtonImage;

                if (!defenderButton.IsShovelButton)
                {
                    defenderButton.GetComponentsInChildren<Image>().Single(img => img.transform.childCount == 0).color = Color.black;
                }
            }
            if (!IsShovelButton)
            {
                defenderImage.color = Color.white;
                SelectedDefenderPrefabs = DefenderPrefabs;
                ShovelActive = false;
            }
            else
            {
                ShovelActive = true;
            }

            buttonImage.sprite = ButtonActiveImage;
        }
    }
}