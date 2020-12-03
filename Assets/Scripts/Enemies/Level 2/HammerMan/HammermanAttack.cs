using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammermanAttack : MonoBehaviour
{
    public EnemyMovement movement;  //get the enemymovement script
    private Animator animator;
    private EnemyStat enemyStat;//Get enemy's stat to get their damage output
    public Transform attackPoint;   //get attackpoint
    public float attackRange; //attack range of the hammerman
    public LayerMask playerlayer;
    void Start()
    {
        attackRange = 0.4f;
        enemyStat = gameObject.GetComponent<EnemyStat>();
        movement = gameObject.GetComponent<EnemyMovement>();
        animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (movement.isInAttackRange && movement.followingPlayer)
        {

            PerformAttack();
        }
    }
    void DamagePlayer()
    {
        animator.ResetTrigger("lift");
        Collider2D[] playerhitted = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerlayer); //capture objects hitted that is player layer in the attackRange
        //if it is not null then hurt the player 
        try
        {
            foreach (Collider2D players in playerhitted)
            {
                players.gameObject.GetComponent<PlayerStat>().SendMessage("decreaseHP", enemyStat.damage);
            }
        }
        catch { }




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
