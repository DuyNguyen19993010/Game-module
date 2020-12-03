using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [HideInInspector]
    private bool mustPatrol;

    private bool mustTurn;
    private int mustfly = 0;
    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public float walkSpeed;
    public Animator animator;
    private float nextActionTime = 0.0f;
    private float period = 3.0f;
    public float distance = 0.2f;
    private Transform targetPlayer;
    public GameObject myObject;
    private bool canspawn = true;
    void Start()
    {
        try
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        catch { }

        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        //this is used to attack every few seconds
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            animator.SetBool("canAttack", true);
        }
        else
        {
            animator.SetBool("canAttack", false);
        }
        //check the next behaviour(flying) which happens every 4 times boss flips.
        if (mustfly == 4)
        {
            animator.SetBool("canMove", false);
            animator.SetBool("canAttack", false);
            mustPatrol = false;
            fly();


        }
        if (mustPatrol)
        {
            animator.SetBool("canMove", true);
            Patrol();
        }
    }
    //teleport boss to the middle air so he floats while the floor is in fire
    void fly()
    {
        transform.position = new Vector3(21, 0.4f, 0f);
        if (canspawn)
        {
            myObject.GetComponent<SpawnFireGround>().Spawn();
            canspawn = false;
        }
        //wait 4 seconds before next line which happens inside executeAfterTime body
        StartCoroutine(ExecuteAfterTime(4));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        //put the boss back on the flor and change his animation so he can move
        transform.position = new Vector3(21, 0.0067f, 0f);
        animator.SetBool("canMove", true);
        mustfly = 0;
        mustPatrol = true;

    }
    //patrol around and change direction before hitting the wall
    void Patrol()
    {

        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
    //this is used just to flip the the oppsite direction
    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustfly++;
        canspawn = true;
        mustPatrol = true;

    }
    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }


    public void DamagePlayer(int damage)
    {
        try
        {
            if (Vector2.Distance(transform.position, targetPlayer.position) <= distance)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>().SendMessage("decreaseHP", damage);
            }
        }
        catch { }
    }
}


