using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public string key;

    void Start()
    {
        Text textComponent = GetComponent<Text>();
        textComponent.text = LocalizationManager.Instance.GetLocalizedValue(key);
    }
}
