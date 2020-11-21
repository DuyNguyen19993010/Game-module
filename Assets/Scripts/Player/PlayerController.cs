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
        jumpHeight = 145.0f;
        facingRight = true;
        groundCheckRadius = 0.16f;
        wallCheckRadius = 0.09f;
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
        checkGround();
        wallCheck();
        wallJump();
        CheckDash();
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
            wallSlide();
            Dash();
            StartCoroutine(Jump());
        }
    }
    void movePlayer(Vector2 my_movement)
    {
        rb.velocity = my_movement;
    }
    IEnumerator Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpHeight));
        }
        yield return new WaitForSeconds(0.01f);

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
        }
        if (isLefttWalled && Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {

            rb.AddForce(new Vector2(10, 2), ForceMode2D.Impulse);
        }
    }
    void wallCheck()
    {
        wallRightRayCast = Physics2D.Raycast(transform.position, Vector2.right, wallCheckRadius, whatIsWall);
        wallLeftRayCast = Physics2D.Raycast(transform.position, Vector2.left, wallCheckRadius, whatIsWall);
        if (wallRightRayCast.collider != null && !isGrounded)
        {
            isRighttWalled = true;

        }
        else
        {

            isRighttWalled = false;
        }

        if (wallLeftRayCast.collider != null && !isGrounded)
        {
            isLefttWalled = true;
        }
        else
        {

            isLefttWalled = false;
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
            StartCoroutine(Jump());



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



}
