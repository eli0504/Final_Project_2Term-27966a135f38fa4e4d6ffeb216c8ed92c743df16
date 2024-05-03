using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finalboss : MonoBehaviour
{
    private Health health;

    private Animator anim;

    public Transform pointA;
    public Transform pointB;
    private Transform currentPoint;
    public Transform player;

    public float speed = 2;
    public float chaseRadius = 5f;
    public float attackRadius = 2f;

    
    //clamp
    public float minY;
    public float maxY;
    public float maxX;
    public float minX;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
        Invoke("SetCurrentPoint", 1f);
    }

    private void SetCurrentPoint()
    {
        currentPoint = pointB;
    }

    private void Update()
    {
        //clamp Y pos

        Vector3 actualYPos = transform.position;

        float newYPos = Mathf.Clamp(actualYPos.y, minY, maxY);

        transform.position = new Vector3(actualYPos.x, newYPos, actualYPos.z);

        //clamp X pos

        Vector3 actualXPos = transform.position;

        float newXPos = Mathf.Clamp(actualXPos.x, minX, maxX);

        transform.position = new Vector3(newXPos, actualXPos.y, actualXPos.z);

        //Stats
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance > chaseRadius)
        {
            Patrol();
        }
        else if (distance > attackRadius)
        {
            Chasing();
        }
        else
        {

             Attack();

        }
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, speed * Time.deltaTime);

    }

    //The enemy will go from one point to another to patrol
    public void Patrol()
    {
        anim.SetBool("run", true);
        //if enemy reach the current point
        if (Vector2.Distance(transform.position, currentPoint.position) < 2f && currentPoint == pointB)
        {
            currentPoint = pointA;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Vector2.Distance(transform.position, currentPoint.position) < 2f && currentPoint == pointA)
        {
            transform.rotation = Quaternion.identity;
            currentPoint = pointB;

        }
        else if (currentPoint == player)
        {
            transform.rotation = Quaternion.identity;
            currentPoint = pointB;
        }
    }

    //When the player approaches the enemy, enemy will chase him
    private void Chasing()
    {
        anim.SetBool("run", true);
        LookAtPlayer();
        currentPoint = player;
    }

    //When the player enters the enemy's attack range, enemy will attack player
    public void Attack()
    {
        LookAtPlayer();
        currentPoint = transform; //stays in the place to attack
        anim.SetBool("run", false);
        anim.SetTrigger("attack");
        audioLibrary.PlaySound("enemy");
    }

    public void LookAtPlayer()
    {
        Vector2 directionToPlayer = player.position - transform.position;

        if (directionToPlayer.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }

    //visual
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.position, 0.5f);

        //chasing
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        //atttack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

}
