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

    //dash ability
    [SerializeField] private float dashVelocity;
    [SerializeField] private float dashTime;
    [SerializeField] private TrailRenderer trailRenderer;
    private float initialGravity;
    private bool canDash = false; // Start as false, only true after picking up ability
    private bool canMove = true;


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        rigidbody2D = GetComponentInChildren<Rigidbody2D>();
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        boxCollider2D = GetComponentInChildren<BoxCollider2D>();
    }

    private void Start()
    {
        initialGravity = rigidbody2D.gravityScale;
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

        //DASH
        if( Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
        
    private void FixedUpdate()
    {
        if (canMove)
        {
            rigidbody2D.velocity = new Vector2(moveSpeed * horizontalInput, rigidbody2D.velocity.y);
        }
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ability"))
        {
            canDash = true;
            Destroy(other.gameObject); 
        }
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

    public IEnumerator Dash()
    {
        /* canMove = false;
         canDash = false;
         rigidbody2D.gravityScale = 0;
         rigidbody2D.velocity = new Vector2(dashVelocity * transform.localScale.x ,0);
         trailRenderer.emitting = true;

         yield return new WaitForSeconds(dashTime);

         canMove = true;
         canDash = true;
         rigidbody2D.gravityScale = initialGravity;
         trailRenderer.emitting = false;*/

        canMove = false;
        rigidbody2D.gravityScale = 0;

        // Determine dash direction based on player's input direction
        float dashDirection = horizontalInput != 0 ? horizontalInput : (transform.localScale.x > 0 ? 1 : -1);
        rigidbody2D.velocity = new Vector2(dashVelocity * dashDirection, 0);
        trailRenderer.emitting = true;

        yield return new WaitForSeconds(dashTime);

        canMove = true;
        rigidbody2D.gravityScale = initialGravity;
        trailRenderer.emitting = false;
    }

    private void RunAnim()
    {
        /*  if (horizontalInput > 0f) //right direction
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
          } */
        if (horizontalInput > 0f) // right direction
        {
            anim.SetBool("running", true);
            transform.localScale = new Vector3(1, 1, 1); // face right
        }
        else if (horizontalInput < 0f) // left direction
        {
            anim.SetBool("running", true);
            transform.localScale = new Vector3(-1, 1, 1); // face left
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

}
