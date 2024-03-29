using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    private Animator animator;
    private KnockBack knockBackScript;

    public Transform attackpoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 40;

    public float attackRate = 3f;
    float nextAttackTime = 0f;


    void Awake() 
    {
        animator = GetComponent<Animator>();
        knockBackScript = GetComponent<KnockBack>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {  
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    { 
        //Play an attack animation
        animator.SetTrigger("Attack");
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);
        //Damage them
        foreach (Collider2D enemies in hitEnemies)
        {
            enemies.GetComponent<Enemies>().TakeDamage(attackDamage);
            knockBackScript.Knockback(enemies.GetComponent<Rigidbody2D>());
        }

    }

    void OnDrawGizmosSelected()
    {
        if (attackpoint == null)
            return;

        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }
}
