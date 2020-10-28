using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    //Player movement and rigidbody 2d
    public Rigidbody2D rb;
    //----------------------------Checking speed and jump height
    public float speed;
    public float speedSlowOffset;
    public float jumpHeight;
    //Check facing direction
    private bool facingRight;
    public Vector2 movement;
    //------------------------------------Check for ground
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    //----------------------------------JUMPING
    public int extraJumps;
    public int extraJumpsValue;
    //-=--------------------------------Player input check
    string buttonPressed;
    //--------------------------------Animation
    public Animator animator;
    public bool isMoving;



    // Start is called before the first frame update
    void Start()
    {
        extraJumpsValue = 3;
        speed = 3.0f;
        jumpHeight = 5.0f;
        facingRight = true;
        speedSlowOffset = 0.4f;
        isMoving = true;
        rb = GetComponent<Rigidbody2D>();

    }
    // Update is called once per frame
    void Update()
    {

        //check the input 
        checkMovementInput();
        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);
        }
        else
        {
            animator.SetBool("isGrounded", false);
        }
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        //check facing direction
        checkFacingDirection();

    }
    void FixedUpdate()
    {

        //Check if player is grounded
        checkGround();
        //Move the player
        if (isMoving)
        {
            movePlayer();
        }
    }
    void checkGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }
    void checkFacingDirection()
    {
        if (rb.velocity.x > 0 && facingRight == false)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight == true)
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
    //------------------------------MOVEMENT METHODS---------------------------------
    void checkMovementInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            buttonPressed = "D";
        }
        else if (Input.GetKey(KeyCode.A))
        {
            buttonPressed = "A";
        }
        else
        {
            buttonPressed = "None";

        }


        if (Input.GetKey(KeyCode.Space) && extraJumps != 0)
        {
            buttonPressed = "Space";
            animator.SetTrigger("takeOff");
        }
        else if (Input.GetKey(KeyCode.Space) && extraJumps == 0 && isGrounded)
        {
            buttonPressed = "Space";
            animator.SetTrigger("takeOff");
        }
    }
    void movePlayer()
    {
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }


        if (buttonPressed == "D")
        {
            rb.velocity = (new Vector2(speed, rb.velocity.y));

        }
        else if (buttonPressed == "A")
        {
            rb.velocity = (new Vector2(-speed, rb.velocity.y));

        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

        }


        if (buttonPressed == "Space" && extraJumps != 0)
        {
            rb.velocity = (new Vector2(rb.velocity.x, jumpHeight));
            slowDown();
            extraJumps--;
        }
        else if (buttonPressed == "Space" && extraJumps == 0 && isGrounded)
        {
            rb.velocity = (new Vector2(rb.velocity.x, jumpHeight));
            slowDown();

        }

    }
    public void setMoving(bool value)
    {
        isMoving = value;
    }
    void slowDown()
    {
        rb.AddForce(new Vector2(rb.velocity.x * -1 * speedSlowOffset, rb.velocity.y));
    }
}
