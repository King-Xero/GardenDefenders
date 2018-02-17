using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public string key;

    private Text textComponent;

    void Awake()
    {
        textComponent = GetComponent<Text>();
    }

    void Start()
    {
        SetText(this, EventArgs.Empty);
        LocalizationManager.Instance.LanguageUpdateEventHandler += SetText;
    }

    private void SetText(object sender, EventArgs eventArgs)
    {
        textComponent.text = LocalizationManager.Instance.GetLocalizedValue(key);
    }
}
