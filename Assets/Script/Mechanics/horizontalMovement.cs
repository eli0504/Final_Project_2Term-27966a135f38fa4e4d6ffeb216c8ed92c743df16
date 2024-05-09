using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horizontalMovement : MonoBehaviour
{
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private float movementVelocity;
    private int nextPlatform = 1;
    private bool platformOrder = true;

    void Update()
    {
        //Platform order (positive or negative)
        if (platformOrder && nextPlatform + 1 >= movementPoints.Length)
        {
            platformOrder = false;
        }

        if(!platformOrder && nextPlatform <= 0)
        {
            platformOrder = true;
        }

        //to change de direction
        if (Vector2.Distance(transform.position, movementPoints[nextPlatform].position) < 0.1f)
        {
            if (platformOrder)
            {
                nextPlatform += 1;
            }
            else
            {
                nextPlatform -= 1;
            }
        }

        //movement
        transform.position = Vector2.MoveTowards(transform.position, movementPoints[nextPlatform].position, movementVelocity * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) //make the player child of the platform, in this way the player will move with platform
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
