using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VolumeComponent;

public class PlayerCombat : MonoBehaviour
{
    private BossHealth bossHealth;
    private EnemyHealth enemyHealth;
    public Animator anim;

    public Transform attackPoint;
   
    public LayerMask enemyLayers;

    [SerializeField] private float damage;

    public float attackRange = 0.5f;

    //time between attacks
    private float timeBetweenAttacks = 1;
    private float timeNextAttack;
   
    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        bossHealth = GetComponent<BossHealth>();
    }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && timeNextAttack <= 0)
        {
            Attack();
            timeNextAttack = timeBetweenAttacks;
        }

        if(timeNextAttack > 0)
        {
            timeNextAttack -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        audioLibrary.PlaySound("attack");

        //detect enemies in range of attack with a circle
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage();
            }

            BossHealth bossHealth = enemy.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.BossDamage();
            }
        }

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage();
        }

        if (bossHealth != null)
        {
            bossHealth.BossDamage();
        }
    }
    //visual
    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
