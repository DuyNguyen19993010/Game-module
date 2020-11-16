using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    // animator
    public Animator animator;
    public Transform attackPoint;
    private bool canAttack;
    private float nextAttackTime;
    public float numberOfAttackPerUnit;
    public float attackRadius;
    public LayerMask playerLayer;
    public GameObject target;
    private Collider2D Player;
    public float damage;
    public float attackFrameSec;
    public float enemyAttackTime;
    public float userAttackTime;

    // Start is called before the first frame update
    void Awake()
    {
        enemyAttackTime = 0;
        target = GameObject.FindGameObjectWithTag("player");
        canAttack = true;
        nextAttackTime = 0;
        animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {


        if (Time.time > nextAttackTime && canAttack && Mathf.Abs(Vector2.Distance(transform.position, target.transform.position)) <= Mathf.Abs(Vector2.Distance(transform.position, attackPoint.position)))
        {
            animator.SetBool("isRunning", false);
            StartCoroutine(Attack());
            nextAttackTime = Time.time + 1 / numberOfAttackPerUnit;
        }

    }
    IEnumerator Attack()
    {
        gameObject.GetComponent<EnemyMovement>().SendMessage("setMoving", false);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackFrameSec);
        enemyAttackTime = Time.time;
        //&& userAttackTime > enemyAttackTime-0.5
        if (userAttackTime < enemyAttackTime && userAttackTime != 0)
        {
            Debug.Log("Enemy Parried");
            Damage();
        }
        else
        {
            Player = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
            try
            {
                Player.GetComponent<Attacks>().SendMessage("Damage", damage);
            }
            catch
            {
                Debug.Log("Exception NUll for enemy:");
            }
        }
        enemyAttackTime = 0;
        userAttackTime = 0;
    }
    void Damage()
    {
        Debug.Log("Enemy hurted");
        canAttack = false;
        animator.SetBool("isRunning", false);
        StartCoroutine("Hurt");
    }
    IEnumerator Hurt()
    {
        animator.SetTrigger("Damage");
        yield return new WaitForSeconds(1);
        canAttack = true;
    }
    void SetPlayerAttackTime(float attackTime)
    {
        userAttackTime = attackTime;
    }
}
