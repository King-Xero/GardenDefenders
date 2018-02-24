using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpText : MonoBehaviour
{
    private Text messageText;

    private string text;
    private bool isFadingOut;

    public string Text
    {
        get { return text; }
        set
        {
            text = value;
            if (messageText)
            {
                messageText.text = value;
            }
        }
    }

    
    void Start ()
    {
        messageText = GetComponent<Text>();
        messageText.text = text;
    }
	
	
    public void DisplayText(float displayTime)
    {
        Debug.Log("Showing Message");
        gameObject.SetActive(true);
        Invoke("HideText", displayTime);
    }

    private void HideText()
    {
        gameObject.SetActive(false);
        Debug.Log("Message Hidden");
    }
}
