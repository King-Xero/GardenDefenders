using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScarecrowDefender : MonoBehaviour {

    [Tooltip("x postion after which enemies become 'visible'")]
    public float VisibleXPosition;

    private Animator animator;
    private EnemyWavesManager enemyWavesManager;
    private GameObject currentTarget;

	
	void Start () {
        animator = GetComponent<Animator>();

	    enemyWavesManager = FindObjectOfType<EnemyWavesManager>();
	    if (enemyWavesManager == null)
	    {
	        Debug.LogError("Enemy waves manager not found");
	    }
    }
	
	
	void Update () {

        if (!currentTarget || !currentTarget.activeInHierarchy)
        {
            animator.SetBool("isAttacking", false);
        }

        if (IsAttackerInSight())
        {
            animator.SetBool("enemyInSight", true);
        }
        else
        {
            animator.SetBool("enemyInSight", false);
        }
	}

    void OnTriggerStay2D(Collider2D col)
    {
        GameObject colliderGameObject = col.gameObject;

        if (!colliderGameObject.GetComponent<Attacker>())
        {
            return;
        }

        Attack(colliderGameObject);
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
                        attacker.position.x >= transform.position.x && attacker.position.x <= VisibleXPosition)
                    {
                        return true;
                    }
                }
                return false;
            });
        }
        return false;
    }

    private void Attack(GameObject colliderGameObject)
    {
        currentTarget = colliderGameObject;

        animator.SetBool("isAttacking", true);
    }

    public void AttackCurrenttarget(float damage)
    {
        Debug.Log("Dealing " + damage + " damage to the current target");
        if (currentTarget)
        {
            Health currentTargetHealth = currentTarget.GetComponent<Health>();
            if (currentTargetHealth)
            {
                currentTargetHealth.InflictDamage(damage);
            }
        }
    }
}
