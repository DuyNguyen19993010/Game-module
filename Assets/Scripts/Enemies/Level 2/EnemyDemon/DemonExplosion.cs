using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonExplosion : MonoBehaviour
{
    //Animator of the Demon
    private Animator animator;
    //Enemymovement script
    private EnemyMovement movement;
    //get the playerstat
    private PlayerStat stats;



    void Start()
    {
        //initialiszing the animator, movement, playerstat
        animator = gameObject.GetComponent<Animator>();
        movement = gameObject.GetComponent<EnemyMovement>();
        stats = GameObject.Find("Player").GetComponent<PlayerStat>();
        //----------------------------------------------------------------
    }

    void Update()
    {
        //disable the movement of demon and trigger the explosion animation
        //if the enemy is in attack range(which means detected player and ready for attacking)
        if (movement.isInAttackRange && movement.followingPlayer)
        {
            animator.SetBool("isRunning", false);
            //disable the moves
            movement.SendMessage("FreezeEnemy");
            //trigger the explosion animation
            animator.SetTrigger("explodes");
        }
        //else keep the demon moving around
        else
        {
            animator.SetBool("isRunning", true);
        }

    }
    //-----------------------------this is getting called through the animation event----------------------------
    void explosionDamage()
    {
        if (Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) < 1)
        {
            stats.SendMessage("decreaseHP", 100);
        }
    }
    //-------------------------------------------------------------------------------------------------------------
    //-----------------------------this is getting called through the animation event at the end to destroy the demon enemy----------------------------
    void destroySelf()
    {
        Destroy(gameObject);
    }
    //---------------------------------------------------------------------------------------------------------------------------
}
