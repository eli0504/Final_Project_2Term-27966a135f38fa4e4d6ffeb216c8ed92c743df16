using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rigidbody;

    public Transform player;

    private bool lookAtRight = true;

    [SerializeField] private float health;
    [SerializeField] HealthBar healthBar;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        healthBar.InitializeHealthBar(health);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void TakeHit(float damage)
    {
        health -= damage;
        healthBar.ChangeActualHealth(health);

        if(health <= 0)
        {
            animator.SetTrigger("death");
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
}
