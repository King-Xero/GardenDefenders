using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class WalkingAttacker : MonoBehaviour
{
    private Attacker attacker;

    // Use this for initialization
    void Start()
    {
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

        attacker.Attack(colliderGameObject);

        Debug.Log("Walking attacker collided with " + col);
    }
}