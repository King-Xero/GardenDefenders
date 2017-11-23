using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScarecrowDefender : MonoBehaviour {

    [Tooltip("x postion after which enemies become 'visible'")]
    public float VisibleXPosition;

    private Animator animator;
    private AttackerSpawner laneSpawner;
    private GameObject currentTarget;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

        laneSpawner = FindObjectsOfType<AttackerSpawner>()
            .Single(spawner => spawner.transform.position.y == gameObject.transform.position.y);
	}
	
	// Update is called once per frame
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
        if (laneSpawner && laneSpawner.AttackerPools != null)
        {
            return laneSpawner.AttackerPools.Any(attackerPool =>
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
