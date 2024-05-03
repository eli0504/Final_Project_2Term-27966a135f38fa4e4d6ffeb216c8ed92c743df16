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

    public float attackRange = 0.5f;

    //time between attacks
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float timeNextAttack;

    [SerializeField]  private float damage;
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
            // Intenta obtener el componente EnemyHealth del enemigo
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage();
            }
            // Intenta obtener el componente BossHealth del enemigo
            BossHealth bossHealth = enemy.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                // Si el componente BossHealth existe, aplica el daño de jefe
                bossHealth.BossDamage();
            }
        }

        // Después del bucle, verifica si hay una referencia al componente EnemyHealth
        if (enemyHealth != null)
        {
            // Si la referencia es válida, aplica el daño al enemigo
            enemyHealth.TakeDamage();
        }

        // También verifica si hay una referencia al componente BossHealth
        if (bossHealth != null)
        {
            // Si la referencia es válida, aplica el daño de jefe
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
