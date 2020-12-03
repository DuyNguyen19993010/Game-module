using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttack : MonoBehaviour
{
    private Transform targetPlayer; // locate the player position
    private float speed;    //speed of the spirit ball
    private float distance; //detect distance between enemy and player
    public Transform firepoint; //locate the firepoint
    public GameObject shootballPrefab;  //get the shots prefab
    private Animator animator;  //animator of the spirit ball
    public bool nextAttack; //decide next attack is avaiable or not
    private bool followingPlayer;   //following player or not
    private Vector3 originalposition;   //the initial position of the enemy
    private bool faceRight; //check facing right or not

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.2f;
        distance = 0.2f;
        animator = gameObject.GetComponent<Animator>();
        nextAttack = true;
        try
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        catch { }
        followingPlayer = false;    //initially set to false
        originalposition = transform.position;
        faceRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if not following a player or once out of chasing player
        //ball moves to its spawning position
        try
        {
            if (followingPlayer == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, originalposition, speed * Time.deltaTime);
            }
            //if player gets close to the attack range of the spirit ball
            // start following player
            if (Vector2.Distance(transform.position, targetPlayer.position) < 0.8)
            {
                followingPlayer = true;
                followPlayer();
            }
            else
            {
                followingPlayer = false;
            }
            //if spirit ball is following the player then it can attack
            if (followingPlayer == true)
            {
                if (nextAttack)
                {
                    shooting();
                    nextAttack = false;
                    StartCoroutine(shootAgain(1.5f));   //make the attack availiable after 1.5s
                }
            }
        }
        catch { }
    }

    //shoot the ball projectile at firepoint position
    void shooting()
    {
        Instantiate(shootballPrefab, firepoint.position, firepoint.rotation);
    }

    // used to perfom following player
    public void followPlayer()
    {
        //if the distance between player and the spirit ball is bigger than default

        if (Vector2.Distance(transform.position, targetPlayer.position) >= distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, speed * Time.deltaTime);   //move tarwards to player position 
        }
        if (transform.position.x - targetPlayer.position.x > 0 && faceRight) //detect if spirit ball is facing player if not flip
        {
            flip();
        }
        else if (transform.position.x - targetPlayer.position.x < 0 && !faceRight)   //detect if spirit ball is facing player if not flip
        {
            flip();
        }
    }
    //use to flip the spirit ball
    void flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }
    //set the next attack availiable after 1.5s
    private IEnumerator shootAgain(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        nextAttack = true;
    }
}
