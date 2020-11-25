using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammermanAttack : MonoBehaviour
{
    public EnemyMovement movement;  //get the enemymovement script
    private Animator animator;
    public Transform attackPoint;   //get attackpoint
    public float attackRange = 0.5f; //attack range of the hammerman
    public LayerMask playerlayer;
    void Start()
    {
        movement = gameObject.GetComponent<EnemyMovement>();
        animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (movement.isInAttackRange && movement.followingPlayer)
        {
            Debug.Log("In range");
            PerformAttack();
        }
    }
    void DamagePlayer()
    {
        animator.ResetTrigger("lift");
        Collider2D[] playerhitted = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerlayer); //capture objects hitted that is player layer in the attackRange
        //if it is not null then hurt the player 
        if (playerhitted != null)
        {
            foreach (Collider2D players in playerhitted)
            {
                players.gameObject.GetComponent<PlayerStat>().SendMessage("decreaseHP", 100);
            }
        }


    }
    private void PerformAttack()
    {
        animator.SetTrigger("lift");
    }

    //Debug with the attack range
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
