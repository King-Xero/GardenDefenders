using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StarsCounter : MonoBehaviour
{
    public enum Status
    {
        Success,
        Failure
    }

    public int StartingStars;
    public int CurrentStars;

    private Text starsCounterText;
    
    // Use this for initialization
    void Start()
    {
        starsCounterText = GetComponent<Text>();

        CurrentStars = StartingStars;
        starsCounterText.text = CurrentStars.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddStars(int stars)
    {
        CurrentStars += stars;
        UpdateDisplay();
        Debug.Log("Adding " + stars + " stars to counter");
    }
    
    public Status UseStars(int stars)
    {
        if (CurrentStars >= stars)
        {
            CurrentStars -= stars;
            UpdateDisplay();
            Debug.Log("Using " + stars + " stars from counter");
            return Status.Success;
        }
        return Status.Failure;
    }

    private void UpdateDisplay()
    {
        starsCounterText.text = CurrentStars.ToString();
    }
}