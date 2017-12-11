using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSelector : MonoBehaviour {

    private Dictionary<LanguageOption, string> languageMap;

    private string selectedLanguageFile;


	// Use this for initialization
	void Start () {
        var languageFileMap = GetComponent<LanguageFileMap>();
        foreach (var language in languageFileMap.Languages)
        {
            languageMap.Add(language.Language, language.FileName);
        }


    }

    // Update is called once per frame
    void Update () {
		
	}

    public void PreviousLanguage()
    {
        
    }

    public void NextLanguage()
    {
        
    }
}
