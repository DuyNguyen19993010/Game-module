using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4Behave : MonoBehaviour
{

    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;
    public float movementSpeed = 10.1f;
    private bool canDamage;
    public float inRange;

    public Rigidbody2D rb;
    public Animator animator;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    private Transform targetPlayer;
        // variable to set the target player later in the code
    void Start()
    {   
        mustPatrol = true;
        //setting the targetplayer to our player
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void Update()
    {
        if(Vector2.Distance(transform.position, targetPlayer.position) < inRange){
            canDamage = true;
        }
        else{
            canDamage = false;
        }
        //check if player is close enough to the enemy

        //check if canDamage is true or false in order to change the animation
        if(canDamage == false){

            if(mustPatrol){
                animator.SetBool("canWalk", true);
                Patrol();
            }
        }
        else{
            animator.SetBool("canWalk", false);
        }
    }

    private void FixedUpdate(){
        if(mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        }
    }
    public void Patrol()
    {
        if(mustTurn || bodyCollider.IsTouchingLayers(groundLayer)){
            Flip();

        }
        rb.velocity = new Vector2(movementSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
// Flip is used to turn around when the enemy is close to wall or edge
    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x *-1, transform.localScale.y);
        movementSpeed *= -1;
        mustPatrol = true;
    }

    //use to damage the player
        public void DamagePlayer(int damage){
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>().SendMessage("decreaseHP", damage);
    }


}
