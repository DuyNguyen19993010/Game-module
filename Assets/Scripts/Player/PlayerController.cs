using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    //Player movement and rigidbody 2d
    public Rigidbody2D rb;
    //----------------------------Checking speed and jump height
    public float speed;
    public float jumpHeight;
    //Check facing direction
    private bool facingRight;
    public Vector2 movement;
    //------------------------------------Check for ground
    public bool isGrounded;
    public float groundCheckRadius;
    private RaycastHit2D groundRayCast;
    public LayerMask whatIsGround;
    //-=--------------------------------Player input check
    string buttonPressed;
    //--------------------------------Animation
    public Animator animator;
    public bool canMove;



    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f;
        jumpHeight = 113.0f;
        facingRight = true;
        groundCheckRadius = 0.16f;
        canMove = true;
        animator = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    // Update is called once per frame
    void Update()
    {
        RayCastDebug();
        checkGround();
        checkFacingDirection();
        //check if can move
        if (canMove)
        {
            //check input
            movement = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);

        }
        animator.SetFloat("Speed", Mathf.Abs(movement.x));


    }
    void FixedUpdate()
    {
        if (canMove)
        {
            movePlayer(movement);
        }
    }
    void movePlayer(Vector2 my_movement)
    {
        rb.velocity = my_movement;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpHeight));
        }
    }
    void checkGround()
    {
        groundRayCast = Physics2D.Raycast(transform.position, Vector2.down, groundCheckRadius, whatIsGround);
        if (groundRayCast.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        animator.SetBool("isGrounded", isGrounded);
    }
    void checkFacingDirection()
    {
        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }






    void FreezePlayer()
    {
        rb.velocity = new Vector2(0, 0);
        canMove = false;
    }
    void UnFreezePlayer()
    {
        canMove = true;
    }
    void RayCastDebug()
    {
        Debug.DrawRay(transform.position, Vector2.down * groundCheckRadius, Color.red);
    }

    //------------------------------MOVEMENT METHODS---------------------------------



}
