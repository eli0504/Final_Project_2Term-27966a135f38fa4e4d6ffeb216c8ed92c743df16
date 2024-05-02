using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    private Animator animator;

    public Rigidbody2D rb2D;

    public Transform player;

    private bool lookAtRight = true;

    //HEALTH
    [SerializeField] private float health;
    [SerializeField] private HealthBar healthBar;


    //ATTACK
    [SerializeField] private Transform attackControl;
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackDamage;


    private void Start()
    {
       
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        healthBar.InitializeHealthBar(health);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("distance", distance);
    }

    public void TakeHit(float damage)
    {
        health -= damage;
        healthBar.ChangeActualHealth(health);

        if(health <= 0)
        {
            animator.SetTrigger("death");
            Death();
        }
    }

    public void Attack()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(attackControl.position, attackRadius);
        foreach(Collider2D collision in items)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Health>().GetDamage();
            }
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    public void LookAtPlayer()
    {
        if((player.position.x > transform.position.x && !lookAtRight) || (player.position.x < transform.position.x && lookAtRight))
        {
            lookAtRight = !lookAtRight;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackControl.position, attackRadius);
    }
}
