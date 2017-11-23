using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    public GameObject Parent;
    public int StarsAmount;

    private StarsCounter starsCounter;
    private Animator animator;

	// Use this for initialization
	void Start () {
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

    // Update is called once per frame
    void Update () {
		
	}

    private void OnMouseDown()
    {
        if (starsCounter)
        {
            starsCounter.AddStars(StarsAmount);
        }
        animator.SetTrigger("collectedTrigger");
    }
}
