using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    //ATTACK POINT
    [SerializeField] private Transform attackControl;
    [SerializeField] private float swordRadius;
    [SerializeField] private float attackDamage;

    private void SwordDamage()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(attackControl.position, swordRadius);
       foreach (Collider2D collision in items)
       {
           if (collision.CompareTag("Player"))
           {
               collision.GetComponent<Health>().GetDamage();
           }
       }
    }

    //visual
    private void OnDrawGizmos()
    {
        //sword
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackControl.position, swordRadius);
    }
}
