using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//---------------------------------All of the code below is my own-----------------------------
//---------------------------------Any Reused code from other people will be documented-------------------
public class PlayerController : MonoBehaviour

{
    //Player movement and rigidbody 2d
    [Header("Rigidbody 2d")]
    public Rigidbody2D rb;
    //----------------------------Checking speed and jump height--------------
    [Header("Movement speed and jump height")]
    public float speed;
    public float jumpHeight;
    //Check facing direction
    private bool facingRight;
    public Vector2 movement;
    //------------------------------------Check for ground-------------------
    [Header("Ground check")]
    public bool isGrounded;
    public float groundCheckRadius;
    private RaycastHit2D groundRayCast;
    public LayerMask whatIsGround;
    //------------Used to let player a jump slightly after they are no longer grounded
    public float hangTime = 0.2f;
    private float hangCounter;
    //------------Used to let player a jump slightly before they are grounded
    public float jumpBufferLength = 0.1f;
    private float jumpBufferCount;

    //-=--------------------------------Wall slide---------------------
    [Header("Wall slide")]
    public bool isLefttWalled;
    public bool isRighttWalled;
    public float wallCheckRadius;
    public float wallSlideSpeed;
    private RaycastHit2D wallLeftRayCast;
    private RaycastHit2D wallRightRayCast;
    public LayerMask whatIsWall;
    //-=--------------------------------Dash---------------------
    [Header("Dash")]
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100;
    private bool isDashing;
    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashCoolDown;
    public bool isPressed = false;


    //-=--------------------------------Player input check
    string buttonPressed;
    //--------------------------------Animation
    [Header("Animator")]
    public Animator animator;
    [Header("Boolean to check if moving is allowed")]
    public bool canMove;



    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f;
        jumpHeight = 3.0f;
        facingRight = true;
        groundCheckRadius = 0.16f;
        wallCheckRadius = 0.07f;
        wallSlideSpeed = 0.1f;
        canMove = true;
        dashTime = 0.2f;
        dashSpeed = 5;


        distanceBetweenImages = 0.1f;
        dashCoolDown = 2.5f;
        animator = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    // Update is called once per frame
    void Update()
    {
        RayCastDebug();
        //check if can move
        if (canMove)
        {
            //Check ground time to let player jump before grounded
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpBufferCount = jumpBufferLength;
            }
            else
            {
                jumpBufferCount -= Time.deltaTime;
            }
            //Check how long has been ungrounded to let the player jump
            if (isGrounded)
            {
                hangCounter = hangTime;
            }
            else
            {
                hangCounter -= Time.deltaTime;
            }
            Jump();
        }
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));


    }
    void FixedUpdate()
    {
        checkFacingDirection();
        CheckDash();
        wallCheck();
        wallJump();
        checkGround();
        if (canMove)
        {
            movePlayer();
            Dash();
            wallSlide();
        }
    }
    void movePlayer()
    {

        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);

    }
    void Jump()
    {
        if (jumpBufferCount >= 0f && hangCounter > 0f)
        {
            rb.velocity = Vector2.up * jumpHeight;
            jumpBufferCount = 0;
        }
    }
    void wallSlide()
    {
        if (rb.velocity.y < 0)
        {
            if (isRighttWalled)
            {


                rb.velocity = new Vector2(rb.velocity.x, wallSlideSpeed);
            }

            if (isLefttWalled)
            {

                rb.velocity = new Vector2(rb.velocity.x, wallSlideSpeed);

            }
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
    void wallJump()
    {
        if (isRighttWalled && Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {
            rb.AddForce(new Vector2(-10, 2), ForceMode2D.Impulse);
            animator.SetBool("isWallSliding", false);
        }
        if (isLefttWalled && Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {

            rb.AddForce(new Vector2(10, 2), ForceMode2D.Impulse);
            animator.SetBool("isWallSliding", false);
        }
    }
    void wallCheck()
    {
        wallRightRayCast = Physics2D.Raycast(transform.position, Vector2.right, wallCheckRadius, whatIsWall);
        wallLeftRayCast = Physics2D.Raycast(transform.position, Vector2.left, wallCheckRadius, whatIsWall);
        if (wallRightRayCast.collider != null && !isGrounded && Input.GetKey(KeyCode.D))
        {
            isRighttWalled = true;

        }
        else
        {

            isRighttWalled = false;
        }

        if (wallLeftRayCast.collider != null && !isGrounded && Input.GetKey(KeyCode.A))
        {
            isLefttWalled = true;
        }
        else
        {

            isLefttWalled = false;
        }
        if (isLefttWalled || isRighttWalled)
        {
            animator.SetBool("isWallSliding", true);

        }
        else
        {
            animator.SetBool("isWallSliding", false);
        }

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

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Time.time >= (lastDash + dashCoolDown)) { }
            AttemptToDash();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isPressed = false;
        }
    }
    void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }
    private void CheckDash()
    {
        isPressed = true;

        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                FreezePlayer();

                if (transform.localScale.x > 0)
                {
                    rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
                }
                dashTimeLeft -= Time.deltaTime;
                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }
            if (dashTimeLeft <= 0 || isRighttWalled || isLefttWalled)
            {
                isDashing = false;
                UnFreezePlayer();

            }
            Jump();



        }
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
        //Wall right check raycast
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.05f), Vector2.right * wallCheckRadius, Color.yellow);
        //Wall left check raycast
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.05f), Vector2.left * wallCheckRadius, Color.yellow);
        //Ground check raycast
        Debug.DrawRay(transform.position, Vector2.down * groundCheckRadius, Color.red);
    }

    //------------------------------MOVEMENT METHODS---------------------------------


    void playRunningSound()
    {

    }


}
