using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour {

    public LanguageFileEntry CurrentSelection;

    private LanguageFileMap languageFileMap;

    private Image flagIcon;

	// Use this for initialization
	void Start () {
        languageFileMap = GetComponent<LanguageFileMap>();

        flagIcon = GetComponent<Image>();

	    var lang = PlayerPrefsManager.GetLanguage();

        if (lang != null)
        {
            CurrentSelection = languageFileMap.Languages.FirstOrDefault(l => l.FileName == lang);
        }
        else
        {
            CurrentSelection = languageFileMap.Languages.First();
        }

        if (CurrentSelection.icon)
        {
            SetFlagIcon(CurrentSelection.icon);
        }
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
        SetFlagIcon(CurrentSelection.icon);
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
        SetFlagIcon(CurrentSelection.icon);
    }

    private void SetFlagIcon(Sprite icon){
        if (flagIcon)
        {
            flagIcon.sprite = icon;
        }
    }
}
