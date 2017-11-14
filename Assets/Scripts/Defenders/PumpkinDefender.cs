using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PumpkinDefender : MonoBehaviour {

    [Tooltip("Distance forward of the defender within which enemies become 'visible'")]
    public float ForwardVisibilityDistance;
    [Tooltip("Distance backward of the defender within which enemies become 'visible'")]
    public float BackwardVisibilityDistance;
    [Tooltip("Range within enemies are destoryed in explosion")]
    public float ExplosionRange;

    private Animator animator;
    private AttackerSpawner laneSpawner;
    private IEnumerable<Attacker> currentTargets;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

        laneSpawner = FindObjectsOfType<AttackerSpawner>()
            .Single(spawner => spawner.transform.position.y == transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
        if (IsAttackerInSight())
        {
            animator.SetBool("enemyInSight", true);
        }
        else
        {
            animator.SetBool("enemyInSight", false);
        }
	}

    private bool IsAttackerInSight()
    {
        if (laneSpawner)
        {
            if (laneSpawner.transform.childCount > 0)
            {
                foreach (Transform attacker in laneSpawner.transform)
                {
                    if (attacker.position.x <= transform.position.x + ForwardVisibilityDistance &&
                        attacker.position.x >= transform.position.x - BackwardVisibilityDistance)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var attackers = FindObjectsOfType<Attacker>().Where(attacker => CheckAttackRange(attacker));

        Attack(attackers);

        Debug.Log("Walking attacker collided with " + col);
    }

    private bool CheckAttackRange(Attacker attacker)
    {
        return attacker.transform.position.y == transform.position.y &&
                       (attacker.transform.position.x <= transform.position.x + ExplosionRange &&
                        attacker.transform.position.x >= transform.position.x - ExplosionRange);
    }

    private void Attack(IEnumerable<Attacker> attackersInRange)
    {
        currentTargets = attackersInRange;

        animator.SetTrigger("attackTrigger");
    }

    public void DestroyCurrentTargets()
    {
        Debug.Log("Destroying Objects " + currentTargets);

        if (currentTargets != null)
        {
            foreach (var attacker in currentTargets)
            {
                Destroy(attacker.gameObject);
            }
        }
    }
}
