using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LanguageSelector : MonoBehaviour {

    public LanguageFileEntry CurrentSelection;

    private LanguageFileMap languageFileMap;

    private SpriteRenderer flagIcon;

	// Use this for initialization
	void Start () {
        languageFileMap = GetComponent<LanguageFileMap>();

        flagIcon = GetComponent<SpriteRenderer>();

        CurrentSelection = languageFileMap.Languages.FirstOrDefault(lang => lang.FileName == PlayerPrefsManager.GetLanguage());

        SetFlagIcon(CurrentSelection.icon);

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void PreviousLanguage()
    {
        int index = languageFileMap.Languages.IndexOf(CurrentSelection);

        if (index == 0)
        {
            CurrentSelection = languageFileMap.Languages.LastOrDefault();
        }
        else
        {
            CurrentSelection = languageFileMap.Languages.ElementAt(index - 1);
        }
    }

    public void NextLanguage()
    {
        int index = languageFileMap.Languages.IndexOf(CurrentSelection);

        if (index >= languageFileMap.Languages.Count() - 1)
        {
            CurrentSelection = languageFileMap.Languages.FirstOrDefault();
        }
        else
        {
            CurrentSelection = languageFileMap.Languages.ElementAt(index + 1);
        }
    }

    private void SetFlagIcon(Sprite icon){
        if (flagIcon)
        {
            flagIcon.sprite = icon;
        }
    }
}
