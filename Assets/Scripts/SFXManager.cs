using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour {

    public AudioClip DefenderPlaced;
    public AudioClip DefenderRemoved;
    public AudioClip LevelComplete;
    public AudioClip LevelFailed;
    public AudioClip StarCollected;
    public AudioClip WaveStarted;

    private AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("Don't destroy on load: " + name);
    }

	
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	
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
