using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Range(0, 200)] public float HealthPoints;
    public EventHandler OnHealthDecreased = (sender, e) => { };

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InflictDamage(float damage)
    {
        HealthPoints -= damage;
        OnHealthDecreased.Invoke(this, EventArgs.Empty);
        if (HealthPoints <= 0)
        {
            //Death animation can go here
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}