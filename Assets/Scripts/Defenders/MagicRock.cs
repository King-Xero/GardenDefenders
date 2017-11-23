using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRock : MonoBehaviour {

    private Animator anim;
    private GameObject currentTarget;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentTarget || !currentTarget.activeInHierarchy)
        {
            anim.SetBool("isAttacked", false);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        Attacker attacker = col.gameObject.GetComponent<Attacker>();

        JumpingAttacker jumpAttacker = col.gameObject.GetComponent<JumpingAttacker>();

        if (!jumpAttacker && attacker)
        {
            currentTarget = col.gameObject;
            anim.SetBool("isAttacked", true);
        }
    }

}
