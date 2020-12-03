using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy3 : MonoBehaviour
{
    // variable to set the target player later in the code
    private Transform targetPlayer;

    //when the enemy detects the player
    public float DetectRange = 0.8f;
    //to  access the animator of the gameObject
    public Animator animator;

    void Start()
    {
        //setting the targetplayer to our player
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (transform.position.x < targetPlayer.position.x)
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            }
            else if (transform.position.x > targetPlayer.position.x)
            {
                transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);

            }

            //if statement to asure we keep a distance between player and enemy2
            if (Vector2.Distance(transform.position, targetPlayer.position) < DetectRange)
            {
                animator.SetBool("canAttack", true);
            }
            else
            {
                animator.SetBool("canAttack", false);
            }
        }
        catch { }

    }
}
