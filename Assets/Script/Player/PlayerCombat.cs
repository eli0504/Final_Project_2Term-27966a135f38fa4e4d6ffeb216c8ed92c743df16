using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    public Animator anim;

    public Transform attackPoint;
   
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        audioLibrary.PlaySound("attack");

        //detect enemies in range of attack with a circle
        Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage();
        }
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage();
        }
          
    }

    //visual
    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
