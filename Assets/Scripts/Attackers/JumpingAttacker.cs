﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class JumpingAttacker : MonoBehaviour
{
    private Animator animator;
    private Attacker attacker;

    
    void Start()
    {
        animator = GetComponent<Animator>();
        attacker = GetComponent<Attacker>();
    }

    
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject colliderGameObject = col.gameObject;

        if (!colliderGameObject.GetComponent<Defender>())
        {
            return;
        }

        if (colliderGameObject.GetComponent<StoneDefender>() || colliderGameObject.GetComponent<MagicRock>() || colliderGameObject.GetComponent<EnhancedMagicRock>())
        {
            animator.SetTrigger("jumpTrigger");
        }
        else
        {
            attacker.Attack(colliderGameObject);
        }

        Debug.Log("Jumper collided with " + col);
    }
}