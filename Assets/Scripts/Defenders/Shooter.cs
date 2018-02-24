using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject Gun;
    public GameObject Projectile;

    private GameObject projectileParent;
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

        projectileParent = GameObject.Find("Projectiles");

        if (!projectileParent)
        {
            projectileParent = new GameObject("Projectiles");
        }
    }

    
    void Update()
    {
        if (IsAttackerInSight())
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void Fire()
    {
        Instantiate(Projectile, Gun.transform.position, Quaternion.identity, projectileParent.transform);
    }

    private bool IsAttackerInSight()
    {
        if (enemyWavesManager && enemyWavesManager.EnemyPools != null)
        {
            return enemyWavesManager.EnemyPools.Any(attackerPool =>
            {
                foreach (Transform attacker in attackerPool.transform)
                {
                    if (attacker.gameObject.activeInHierarchy && attacker.transform.position.y == transform.position.y)
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