using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBossFlip : MonoBehaviour
{
    public Transform player;
    public Transform attackPoint;

    public float attackRange = 0.5f;
    //public bool isFlipped = false;
    private bool isFlipped = true;
  //  private bool lookAtRight = true;
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }

       /* if((player.position.x > transform.position.x && !lookAtRight) || (player.position.x < transform.position.x && lookAtRight))
        {
            lookAtRight = !lookAtRight;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }*/
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
