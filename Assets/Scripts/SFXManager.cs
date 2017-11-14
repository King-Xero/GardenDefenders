using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour {

    public AudioClip DefenderPlaced;
    public AudioClip DefenderRemoved;
    public AudioClip LevelEnd;

    private AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("Don't destroy on load: " + name);
    }

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void PlayClip(AudioClip selectedClip)
    {
        audioSource.PlayOneShot(selectedClip);
    }
}
