using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAbility : MonoBehaviour
{
    [SerializeField] private float daño;
    [SerializeField] private Vector2 boxDimensions;
    [SerializeField] private Transform boxPosition;
    [SerializeField] private float timeLife;

    private void Start()
    {
        Destroy(gameObject, timeLife);
    }

    public void Damage()
    {
        Collider2D[] items = Physics2D.OverlapBoxAll(boxPosition.position, boxDimensions, 0f);

        foreach (Collider2D collision in items)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Health>().GetDamage();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxPosition.position, boxDimensions);
    }
}
