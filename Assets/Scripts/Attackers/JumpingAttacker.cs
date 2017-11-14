using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class JumpingAttacker : MonoBehaviour
{
    private Animator animator;
    private Attacker attacker;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        attacker = GetComponent<Attacker>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject colliderGameObject = col.gameObject;

        if (!colliderGameObject.GetComponent<Defender>())
        {
            return;
        }

        if (colliderGameObject.GetComponent<StoneDefender>())
        {
            animator.SetTrigger("jumpTrigger");
        }
        else
        {
            attacker.Attack(colliderGameObject);
        }

        Debug.Log("Jumper collided with " + col);
    }
}