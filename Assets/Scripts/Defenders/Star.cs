using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    public GameObject Parent;
    public int StarsAmount;

    private StarsCounter starsCounter;
    private Animator animator;
    private SFXManager sfxManager;

    
    void Start ()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        starsCounter = FindObjectOfType<StarsCounter>();
        if (!starsCounter)
        {
            Debug.Log("Stars counter not found");
        }
        if (!Parent)
        {
            Debug.Log("Parent object nt set.");
        }
        else
        {
            animator = Parent.GetComponent<Animator>();
            if (!animator)
            {
                Debug.Log("Animator not found.");
            }
        }
    }

    
    void Update () {
		
	}

    public void OnMouseDown()
    {
        Debug.Log("Star Collected");
        if (starsCounter)
        {
            starsCounter.AddStars(StarsAmount);
            if (sfxManager)
            {
                sfxManager.PlayClip(sfxManager.StarCollected);
            }
        }
        animator.SetTrigger("collectedTrigger");
    }
}
