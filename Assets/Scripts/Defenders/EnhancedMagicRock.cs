using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnhancedMagicRock : MonoBehaviour {

    [Tooltip("Distance forward of the defender within which enemies become 'visible'")]
    public float ForwardVisibilityDistance;

    private Animator animator;
    private AttackerSpawner laneSpawner;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();

        laneSpawner = FindObjectsOfType<AttackerSpawner>()
            .Single(spawner => spawner.transform.position.y == transform.position.y);
    }

    // Update is called once per frame
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
        if (laneSpawner && laneSpawner.AttackerPools != null)
        {
            return laneSpawner.AttackerPools.Any(attackerPool =>
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
