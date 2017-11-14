using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public AudioClip[] LevelMusicChangeArray;

    private AudioSource audioSource;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("Don't destroy on load: " + name);
    }

	void Start ()
	{
	    audioSource = GetComponent<AudioSource>();
	}

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip thisLevelMusic = LevelMusicChangeArray[scene.buildIndex];
        Debug.Log("Playing clip: " + thisLevelMusic);

        if (thisLevelMusic && audioSource.clip != thisLevelMusic)
        {
            audioSource.clip = thisLevelMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
	
	void Update () {
		
	}

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
