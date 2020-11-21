using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Player detection")]
    //------------------------Player detection
    public float detectRadius;
    private float playerdDistance;
    public bool followingPlayer;
    public LayerMask playerLayer;
    [Header("Enemy movement's attributes")]
    //Enemy movement speed
    public bool isMoving;
    public float speed;
    //direction
    public int direction;
    private bool facingRight;
    //Rigibody
    private Rigidbody2D rb;
    [Header("Check if the enemy is allowed to move")]
    //-------------------------check if the enemy is allowed to move
    public bool canMove;
    [Header("Obstacle detect radius")]
    //----------------------------------RayCast---------------------------------
    public LayerMask whatIsWall;
    public float wallDetectRadius;
    public float ledgeDetectRadius;
    [SerializeField] private RaycastHit2D leftWall;
    [SerializeField] private RaycastHit2D rightWall;
    [SerializeField] private RaycastHit2D leftLedge;
    [SerializeField] private RaycastHit2D rightLedge;

    void Awake()
    {
        direction = 1;
        canMove = true;
        facingRight = true;
        speed = 10;
        detectRadius = 0.5f;
        followingPlayer = false;
        wallDetectRadius = 0.19f;
        ledgeDetectRadius = 0.12f;
        isMoving = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //Raycast debugger
        RayCastDebugger();
        Flip();
        detectPlayer();
        if (canMove)
        {
            Move();
            followPlayer();
        }

    }
    void Move()
    {
        if (!followingPlayer)
        {
            rb.velocity = new Vector2(direction * speed * Time.deltaTime, rb.velocity.y);
            WallCheck();
            LedgeCheck();
        }
    }
    void followPlayer()
    {
        try
        {
            if (followingPlayer)
            {
                canMove = true;

                if (Mathf.Abs(playerdDistance) >= Mathf.Abs(detectRadius - 0.3f))
                {
                    isMoving = true;
                    if (playerdDistance > 0)
                    {
                        direction = 1;

                    }
                    else
                    {
                        direction = -1;
                    }
                    rb.velocity = new Vector2(direction * speed * Time.deltaTime, rb.velocity.y);

                }
                else
                {
                    if (playerdDistance < 0 && facingRight)
                    {
                        facingRight = !facingRight;
                        Vector3 Scaler = transform.localScale;
                        Scaler.x *= -1;
                        transform.localScale = Scaler;
                    }
                    else if (playerdDistance > 0 && !facingRight)
                    {
                        facingRight = !facingRight;
                        Vector3 Scaler = transform.localScale;
                        Scaler.x *= -1;
                        transform.localScale = Scaler;
                    }
                    // rb.velocity = new Vector2(0, 0);
                    isMoving = false;
                }
            }
            else
            {
                isMoving = true;
            }

        }
        catch
        {
            Debug.Log("Movement Script");
            UnFreezeEnemy();
            followingPlayer = false;
            direction = 1;
        }

        // && Mathf.Abs(Vector2.Distance(transform.position, target.transform.position)) > 1


    }
    void detectPlayer()
    {
        try
        {
            playerdDistance = (GameObject.FindWithTag("Player").transform.position.x - transform.position.x);
            if (Mathf.Abs(playerdDistance) < detectRadius)
            {
                followingPlayer = true;
            }
            else
            {
                followingPlayer = false;
            }
        }
        catch
        {
            UnFreezeEnemy();
            followingPlayer = false;
            isMoving = true;
            direction = 1;
        }
    }
    void WallCheck()
    {
        //Wall left raycast
        leftWall = Physics2D.Raycast(transform.position, Vector2.left, wallDetectRadius, whatIsWall);
        //Wall right raycast
        rightWall = Physics2D.Raycast(transform.position, Vector2.right, wallDetectRadius, whatIsWall);
        if (leftWall.collider != null)
        {
            if (!followingPlayer)
            {
                direction = 1;
            }
            else
            {
                direction = 0;
            }
        }
        if (rightWall.collider != null)
        {
            if (!followingPlayer)
            {
                direction = -1;
            }
            else
            {
                direction = 0;
            }
        }
    }
    void LedgeCheck()
    {
        leftLedge = Physics2D.Raycast(new Vector2(transform.position.x - 0.1f, transform.position.y), Vector2.down, ledgeDetectRadius, whatIsWall);
        rightLedge = Physics2D.Raycast(new Vector2(transform.position.x + 0.1f, transform.position.y), Vector2.down, ledgeDetectRadius, whatIsWall);
        if (leftLedge.collider == null)
        {
            if (!followingPlayer)
            {
                direction = 1;
            }
            else
            {
                direction = 0;
            }
        }
        if (rightLedge.collider == null)
        {
            if (!followingPlayer)
            {
                direction = -1;
            }
            else
            {
                direction = 0;
            }

        }
    }
    void Flip()
    {
        if (rb.velocity.x > 0.0 && !facingRight)
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }
        else if (rb.velocity.x < 0.0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

    }
    void FreezeEnemy()
    {
        rb.velocity = new Vector2(0, 0);
        canMove = false;
    }
    void UnFreezeEnemy()
    {
        canMove = true;
    }
    void RayCastDebugger()
    {
        //-----------------------left wall check
        Debug.DrawRay(transform.position, Vector2.left * wallDetectRadius, Color.red);
        //-----------------------Right wall check
        Debug.DrawRay(transform.position, Vector2.right * wallDetectRadius, Color.red);

        //-----------------------left ledge check
        Debug.DrawRay(new Vector2(transform.position.x - 0.1f, transform.position.y), Vector2.down * ledgeDetectRadius, Color.green);
        //-----------------------Right ledge check
        Debug.DrawRay(new Vector2(transform.position.x + 0.1f, transform.position.y), Vector2.down * ledgeDetectRadius, Color.green);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
