using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    private LevelSceneManager levelManager;

    
    void Start()
    {
        levelManager = FindObjectOfType<LevelSceneManager>();
    }

    
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Attacker>())
        {
            levelManager.LoadLoseScene();
        }
    }
}