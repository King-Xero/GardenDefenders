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
    private AttackerSpawner laneSpawner;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();

        laneSpawner = FindObjectsOfType<AttackerSpawner>()
            .Single(spawner => spawner.transform.position.y == gameObject.transform.position.y);

        projectileParent = GameObject.Find("Projectiles");

        if (!projectileParent)
        {
            projectileParent = new GameObject("Projectiles");
        }
    }

    // Update is called once per frame
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
        if (laneSpawner && laneSpawner.AttackerPools != null)
        {
            return laneSpawner.AttackerPools.Any(attackerPool =>
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