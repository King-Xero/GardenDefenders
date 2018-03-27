using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    public UIOverlayManager UIOverlay;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Attacker>())
        {
            //Disable game functionality
            Debug.Log("Level Failed");
            UIOverlay.ShowPanel(LevelEndCondition.LevelFailed);
        }
    }
}