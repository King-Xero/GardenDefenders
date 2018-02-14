using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftyBotDefender : MonoBehaviour {

    [Tooltip("range which enemies make bot 'alerted'")]
    public float AlertRange;
    [Tooltip("range which enemies make bot 'nervous'")]
    public float NervousRange;

    private Animator animator;
    private EnemyWavesManager enemyWavesManager;
    private float movementSpeed;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();

        enemyWavesManager = FindObjectOfType<EnemyWavesManager>();
        if (enemyWavesManager == null)
        {
            Debug.LogError("Enemy waves manager not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("isAttacking"))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }

        if (AttackerInRange(AlertRange))
        {
            animator.SetBool("isAlerted", true);
        }
        else
        {
            animator.SetBool("isAlerted", false);
        }

        if (AttackerInRange(NervousRange))
        {
            animator.SetBool("isNervous", true);
        }
        else
        {
            animator.SetBool("isNervous", false);
        }
    }

    public void SetSpeed(float speed)
    {
        movementSpeed = speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject colliderGameObject = col.gameObject;

        if (!colliderGameObject.GetComponent<Attacker>())
        {
            return;
        }

        Attack(colliderGameObject);

        //Destroy(colliderGameObject);

        colliderGameObject.GetComponent<Health>().DestroyObject();

        Debug.Log("Walking attacker collided with " + col);
    }

    private bool AttackerInRange(float range)
    {
        if (enemyWavesManager && enemyWavesManager.EnemyPools != null)
        {
            return enemyWavesManager.EnemyPools.Any(attackerPool =>
            {
                foreach (Transform attacker in attackerPool.transform)
                {
                    if (attacker.gameObject.activeInHierarchy && attacker.transform.position.y == transform.position.y &&
                        attacker.position.x >= transform.position.x && attacker.position.x <= range)
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
        animator.SetBool("isAttacking", true);
    }
}
