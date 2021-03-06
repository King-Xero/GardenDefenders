﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnhancedMagicRock : MonoBehaviour {

    [Tooltip("Distance forward of the defender within which enemies become 'visible'")]
    public float ForwardVisibilityDistance;

    private Animator animator;
    private EnemyWavesManager enemyWavesManager;

    
    void Start()
    {
        animator = GetComponent<Animator>();

        enemyWavesManager = FindObjectOfType<EnemyWavesManager>();
        if (enemyWavesManager == null)
        {
            Debug.LogError("Enemy waves manager not found");
        }
    }

    
    void Update()
    {
        if (IsAttackerInSight())
        {
            animator.SetBool("isAlerted", true);
        }
        else
        {
            animator.SetBool("isAlerted", false);
        }
    }

    private bool IsAttackerInSight()
    {
        if (enemyWavesManager && enemyWavesManager.EnemyPools != null)
        {
            return enemyWavesManager.EnemyPools.Any(attackerPool =>
            {
                foreach (Transform attacker in attackerPool.transform)
                {
                    if (attacker.gameObject.activeInHierarchy && attacker.transform.position.y == transform.position.y &&
                        attacker.position.x >= transform.position.x && attacker.position.x <= transform.position.x + ForwardVisibilityDistance)
                    {
                        return true;
                    }
                }
                return false;
            });
        }
        return false;
    }
}
