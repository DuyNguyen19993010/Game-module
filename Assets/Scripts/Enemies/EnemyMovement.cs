using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Enemy's player detection
    public Transform attackPoint;
    public float detectRadius;
    //Enemy movement speed
    public float moveSpeed;
    //direction
    private bool facingRight;
    private SpriteRenderer playerSprite;
    //target to move to
    public Collider2D target;
    //Animator
    public Animator animator;
    public bool canMove;

    void Awake()
    {

        canMove = true;
        facingRight = true;
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        animator = transform.GetComponent<Animator>();

    }
    void Update()
    {
        Flip();
        if (canMove)
        {
            Move();
        }

    }
    void Move()
    {
        // && Mathf.Abs(Vector2.Distance(transform.position, target.transform.position)) > 1
        if (Vector2.Distance(transform.position, target.transform.position) < detectRadius && Mathf.Abs(Vector2.Distance(transform.position, target.transform.position)) >= Mathf.Abs(Vector2.Distance(transform.position, attackPoint.position)) && canMove)
        {

            animator.SetBool("isRunning", true);
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

    }
    void Flip()
    {
        if ((target.transform.position.x - transform.position.x) > 0.0 && !facingRight)
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }
        else if ((target.transform.position.x - transform.position.x) < 0.0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;

        }

    }
    void setMoving(bool value)
    {
        canMove = value;
    }
}
