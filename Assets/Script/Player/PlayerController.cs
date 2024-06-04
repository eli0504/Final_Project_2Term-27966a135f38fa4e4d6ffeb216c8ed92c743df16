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
    public float moveSpeed = 10f;

    bool lookAtRight = true;

    //JUMP CONDITIONS
    public int jumpsMade = 0;
    public int maxJumps = 2;
    public float jumpSpeed = 10f;

    //dash ability
    public Image shiftImageBlocked;
    public Sprite shiftImageUnlocked;
    public GameObject skillPanel;

    private float dashVelocity = 30;
    private float dashTime = 0.2f;
    private float initialGravity;
    private static bool canDash = false;
    private bool canMove = true;
    [SerializeField] private TrailRenderer trailRenderer;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        rigidbody2D = GetComponentInChildren<Rigidbody2D>();
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        boxCollider2D = GetComponentInChildren<BoxCollider2D>();

        QuitRememberPanel();
    }

    private void Start()
    {
        initialGravity = rigidbody2D.gravityScale;
    }

    private void Update()
    {
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

        // animation states
        if (horizontalInput != 0)
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }
    }
        
    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (canMove)
        {
            rigidbody2D.velocity = new Vector2(moveSpeed * horizontalInput, rigidbody2D.velocity.y);
        }

        if (horizontalInput > 0f && !lookAtRight) // right direction
        {
            Flip(); // face right 
        }
        else if (horizontalInput < 0f && lookAtRight) // left direction
        {
            Flip(); // face left
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ability"))
        {
            ShowRememberPanel();
            canDash = true;
            Destroy(other.gameObject);
            StartCoroutine(QuitRememberPanel());
        }
        if(canDash)
        {
            shiftImageBlocked.sprite = shiftImageUnlocked;
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

    public void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        lookAtRight = !lookAtRight;
    }

    public void ShowRememberPanel()
    {

        skillPanel.SetActive(true);

    }

    private IEnumerator QuitRememberPanel()
    {
        yield return new WaitForSeconds(3);
        skillPanel.SetActive(false);
    }
}
