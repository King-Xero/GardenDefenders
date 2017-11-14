using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Attacker : MonoBehaviour {

    [Tooltip("Average time in seconds between spawns")]
    public float SpawnTime;

    private float movementSpeed;
    private GameObject currentTarget;
    private Animator animator;

    // Use this for initialization
    void Start ()
	{
	    animator = GetComponent<Animator>();
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);

	    if (!currentTarget)
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
}
