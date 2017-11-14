using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [Range(0, 5)]
    public float Speed;
    [Range(0, 100)]
    public float Damage;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Attacker attacker = col.gameObject.GetComponent<Attacker>();
        Health health = col.gameObject.GetComponent<Health>();

        if (attacker && health)
        {
            health.InflictDamage(Damage);
            Destroy(gameObject);
        }
    }
}
