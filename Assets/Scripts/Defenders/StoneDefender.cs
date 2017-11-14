using System.Linq;
using UnityEngine;

public class StoneDefender : MonoBehaviour
{
    public Sprite[] DamageSprites;

    private Animator anim;
    private Health health;
    private SpriteRenderer spriteRend;
    private float maxHealth;

	// Use this for initialization
	void Start ()
	{
	    anim = GetComponent<Animator>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        health = GetComponent<Health>();
        maxHealth = health.HealthPoints;
        health.OnHealthDecreased += Rock_OnHealthDecreased;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D col)
    {
        Attacker attacker = col.gameObject.GetComponent<Attacker>();

        JumpingAttacker jumpAttacker = col.gameObject.GetComponent<JumpingAttacker>();

        if (!jumpAttacker && attacker)
        {
            anim.SetTrigger("attackedTrigger");
        }
    }

    void Rock_OnHealthDecreased(object sender, System.EventArgs e)
    {
        Debug.Log("Stone Attacked");
        if (DamageSprites.Any())
        {
            if (health.HealthPoints >= maxHealth * 0.8)
            {
                spriteRend.sprite = DamageSprites[0];
            }
            else if (health.HealthPoints < maxHealth * 0.8 && health.HealthPoints >= maxHealth * 0.6)
            {
                spriteRend.sprite = DamageSprites[1];
            }
            else if (health.HealthPoints < maxHealth * 0.6 && health.HealthPoints >= maxHealth * 0.4)
            {
                spriteRend.sprite = DamageSprites[2];
            }
            else if (health.HealthPoints < maxHealth * 0.4 && health.HealthPoints >= maxHealth * 0.2)
            {
                spriteRend.sprite = DamageSprites[3];
            }
            else if (health.HealthPoints < maxHealth * 0.2)
            {
                spriteRend.sprite = DamageSprites[4];
            }
        }
    }
}
