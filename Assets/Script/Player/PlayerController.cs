using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rigidbody2D;
    private Animator anim;
    
    [SerializeField] private LayerMask groundLayerMask;

    private float horizontalInput;
    private float moveSpeed = 10f;

    //JUMP CONDITIONS
    public int jumpsMade = 0;
    public int maxJumps = 2;
    public float jumpSpeed = 10f;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        rigidbody2D = GetComponentInChildren<Rigidbody2D>();
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        boxCollider2D = GetComponentInChildren<BoxCollider2D>();
    }

    private void Update()
    {
       RunAnim();

       //JUMP
       horizontalInput = Input.GetAxis("Horizontal");

       if (Input.GetKeyDown(KeyCode.Space) && (IsOnTheGround() || jumpsMade < maxJumps))
       {
        rigidbody2D.velocity = Vector2.up * jumpSpeed;
        anim.SetTrigger("jump");
        audioLibrary.PlaySound("jump");
        jumpsMade++;
       }
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(moveSpeed * horizontalInput, rigidbody2D.velocity.y);
    }

    private bool IsOnTheGround()
    {
        float extraHeight = 0.025f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(boxCollider2D.bounds.center,
                                                        Vector2.down,
                                                        boxCollider2D.bounds.extents.y + extraHeight,
                                                        groundLayerMask);
        bool isOnTheGround = raycastHit2D.collider != null;
     
        if (isOnTheGround)
        {
            jumpsMade = 0;
        } 

        //visualice raycast
        Color raycatHitColor = isOnTheGround ? Color.green : Color.red; //ternary operator(if else)
        Debug.DrawRay(boxCollider2D.bounds.center,
                        Vector2.down * (boxCollider2D.bounds.extents.y + extraHeight),
                        raycatHitColor);

        return isOnTheGround;
    }


    private void RunAnim()
    {
        if (horizontalInput > 0f) //right direction
        {
            anim.SetBool("running", true);
            transform.rotation = Quaternion.identity;
        }
        else if (horizontalInput < 0f) //left direction
        {
            anim.SetBool("running", true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            anim.SetBool("running", false);
        } 
    }
}
