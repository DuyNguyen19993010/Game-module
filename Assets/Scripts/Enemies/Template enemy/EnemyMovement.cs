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
    public float attackRangeOffSet;
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
    [Header("Check if is in attack range")]
    //-----------------------------------Check if is in attack rang---------------
    public bool isInAttackRange;
    //----------------------------------RayCast---------------------------------
    public LayerMask whatIsWall;
    public float wallDetectRadius;
    public float wallDetectHeight;
    public float ledgeDetectRadius;
    [SerializeField] private RaycastHit2D wallCheck;
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
        isInAttackRange = false;
        ledgeDetectRadius = 0.21f;
        wallDetectHeight = 0.05f;
        attackRangeOffSet = 0.3f;
        isMoving = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //Raycast debugger
        RayCastDebugger();
        Flip();
        detectPlayer();
        WallCheck();
        LedgeCheck();
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
        }
    }
    void followPlayer()
    {
        try
        {
            if (followingPlayer)
            {
                canMove = true;

                if (Mathf.Abs(playerdDistance) >= Mathf.Abs(detectRadius - attackRangeOffSet))
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
                    isInAttackRange = false;
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
                    isInAttackRange = true;
                    rb.velocity = new Vector2(0, 0);
                    isMoving = false;
                }
            }
            else
            {
                isMoving = true;
                isInAttackRange = false;
            }

        }
        catch
        {
            Debug.Log("Movement Script");
            UnFreezeEnemy();
            followingPlayer = false;
            // direction = 1;
        }



    }
    void detectPlayer()
    {
        try
        {
            float inRange = Vector2.Distance(new Vector2(GameObject.FindWithTag("Player").transform.position.x, GameObject.FindWithTag("Player").transform.position.y), new Vector2(transform.position.x, transform.position.y));
            playerdDistance = (GameObject.FindWithTag("Player").transform.position.x - transform.position.x);
            if (Mathf.Abs(playerdDistance) < detectRadius && inRange < detectRadius)
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
            // direction = 1;
        }
    }
    void WallCheck()
    {
        //Check wall using raycast
        if (transform.localScale.x > 0)
        {
            wallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - wallDetectHeight), Vector2.right, wallDetectRadius, whatIsWall);
            if (wallCheck.collider != null)
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
        else
        {
            wallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - wallDetectHeight), Vector2.left, wallDetectRadius, whatIsWall);
            if (wallCheck.collider != null)
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

        }
    }
    void LedgeCheck()
    {
        //Check ledge
        leftLedge = Physics2D.Raycast(new Vector2(transform.position.x - 0.2f, transform.position.y), Vector2.down, ledgeDetectRadius, whatIsWall);
        rightLedge = Physics2D.Raycast(new Vector2(transform.position.x + 0.2f, transform.position.y), Vector2.down, ledgeDetectRadius, whatIsWall);
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
        //Flip the enemy to the correct direction
        if (rb.velocity.x > 0.0 && !facingRight)
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x = Mathf.Abs(Scaler.x);
            transform.localScale = Scaler;
        }
        else if (rb.velocity.x < 0.0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x = -Mathf.Abs(Scaler.x);
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
        //-----------------------WallCheck

        if (transform.localScale.x > 0)
        {
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - wallDetectHeight), Vector2.right * wallDetectRadius, Color.red);

        }
        if (transform.localScale.x < 0)
        {

            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - wallDetectHeight), Vector2.left * wallDetectRadius, Color.red);

        }

        //-----------------------left ledge check
        Debug.DrawRay(new Vector2(transform.position.x - 0.2f, transform.position.y), Vector2.down * ledgeDetectRadius, Color.green);
        //-----------------------Right ledge check
        Debug.DrawRay(new Vector2(transform.position.x + 0.2f, transform.position.y), Vector2.down * ledgeDetectRadius, Color.green);
    }
    void OnDrawGizmos()
    {
        //Detect range debugging
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
