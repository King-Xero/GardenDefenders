using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Attacker : MonoBehaviour
{
    public string AttackerId;

    [Tooltip("Average time in seconds between spawns")]
    public float SpawnTime;

    private float movementSpeed;
    private GameObject currentTarget;
    private Animator animator;
    private EnemyWavesManager enemyWavesManager;

    // Use this for initialization
    void Start ()
    {
        enemyWavesManager = FindObjectOfType<EnemyWavesManager>();
        if (enemyWavesManager == null)
        {
            Debug.LogError("Enemy Wave Manager not found");
        }
	    animator = GetComponent<Animator>();
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);

        if (!currentTarget || !currentTarget.activeInHierarchy)
	    {
	        animator.SetBool("isAttacking", false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    }

    public void SetSpeed(float speed)
    {
        movementSpeed = speed;
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

    public void Attack(GameObject colliderGameObject)
    {
        currentTarget = colliderGameObject;

        animator.SetBool("isAttacking", true);
    }

    public void AttackerDestroyed()
    {
        enemyWavesManager.EnemyDestroyed(gameObject);
    }
}
