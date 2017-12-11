using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageFileMap : MonoBehaviour
{
    [System.Serializable]
    public class LanguageFileEntry
    {
        public LanguageOption Language;
        public string FileName;
    }
    public LanguageFileEntry[] Languages;
}
