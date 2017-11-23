using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Range(0, 200)] public float HealthPoints;
    public EventHandler OnHealthDecreased = (sender, e) => { };

    private float hp;

    // Use this for initialization
    void Start()
    {
        hp = HealthPoints;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InflictDamage(float damage)
    {
        hp -= damage;
        OnHealthDecreased.Invoke(this, EventArgs.Empty);
        if (hp <= 0)
        {
            //Death animation can go here
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
        hp = HealthPoints;
    }
}