using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageFileMap : MonoBehaviour
{
    public List<LanguageFileEntry> Languages;
}

[System.Serializable]
public class LanguageFileEntry
{
    public LanguageOption Language;
    public string FileName;
    public Sprite icon;
}
