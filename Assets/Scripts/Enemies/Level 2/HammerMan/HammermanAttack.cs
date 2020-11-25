using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammermanAttack : MonoBehaviour
{
    public EnemyMovement movement;  //get the enemymovement script
    private Animator animator;
    public Transform attackPoint;   //get attackpoint
    public float attackRange =0.5f; //attack range of the hammerman
    public LayerMask playerlayer;
    private bool Attacking; // to see if the hammer man is attacking or not, so avoid mutilple attack action at same time
    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.GetComponent<EnemyMovement>();
        animator = gameObject.GetComponent<Animator>();
        Attacking = false; // default to false
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isRunning", movement.isMoving); // animation of running
        // if player is in the attack range, and hammerman is not attacking now
        // start attack actions
        if(movement.isInAttackRange && Attacking == false){
            Attacking = true;
            //movement.SendMessage("FreezeEnemy");
            // movement.canMove = false;
            StartCoroutine(Stage1(1.5f));
        }
    }
    // first stage action of the attack, lift the hammer up
    /* void Stage1(){
        animator.SetBool("lift",true);
        
    } */
    //second stage action of the attack, slam the hammer
    /* void Stage2(){
        animator.SetBool("lift",false);
        animator.SetTrigger("slam");
    } */
    //----animation event that after lift the hammer,goes to stage2 after 1.5s
    /* void ReadyStage2(){
        Invoke("Stage2",1.5f);
    } */
    //----animation event that after slam the hammer, detect if the player is hitted in the attackRange
    void DamagePlayer(){
        Collider2D [] playerhitted = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerlayer); //capture objects hitted that is player layer in the attackRange
        //if it is not null then hurt the player 
        if(playerhitted != null){  
            foreach(Collider2D players in playerhitted){
                players.gameObject.GetComponent<PlayerStat>().SendMessage("decreaseHP",100);
            }
        }
        Attacking = false; // set attacking to false so ready to do next attack
        //movement.SendMessage("UnFreezeEnemy");
        
        //movement.canMove = true;
    }
    private IEnumerator Stage1(float waitTime)
    {
            animator.SetBool("lift",true);
            yield return new WaitForSeconds(waitTime);
            if(Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) < 0.2){
                animator.SetTrigger("slam");
            }
            else{
                animator.SetBool("lift",false);
                Attacking = false;
            }
            
    
    }

    //Debug with the attack range
    void OnDrawGizmosSelected(){
        if(attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    void freeze(){
        movement.SendMessage("FreezeEnemy");
    }
    void unFreeze(){
        movement.SendMessage("UnFreezeEnemy");
    } 
}
