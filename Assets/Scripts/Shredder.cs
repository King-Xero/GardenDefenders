using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    
    void Start()
    {
    }

    
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
    }
}