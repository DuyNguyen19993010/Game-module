using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    // animator
    public Animator animator;
    public Transform attackPoint;
    public float attackDelay;
    private bool canAttack;
    private float nextAttackTime;
    public float numberOfAttackPerUnit;
    public float attackRadius;
    public LayerMask playerLayer;
    public GameObject target;
    private Collider2D Player;
    public float damage;

    // Start is called before the first frame update
    void Awake()
    {
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
            Debug.Log("Attack function calledd");
            animator.SetBool("isRunning", false);
            StartCoroutine(Attack());
            nextAttackTime = Time.time + 1 / numberOfAttackPerUnit;
        }

    }
    IEnumerator Attack()
    {
        Player = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
        if (Player)
        {
            gameObject.GetComponent<EnemyMovement>().SendMessage("setMoving", false);
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(1);
            Debug.Log("Enemy attacking");
            Player.GetComponent<Attacks>().SendMessage("Damage", damage);
        }
        gameObject.GetComponent<EnemyMovement>().SendMessage("setMoving", true);
    }
    void Damage()
    {
        canAttack = false;
        gameObject.GetComponent<EnemyMovement>().SendMessage("setMoving", false);
        animator.SetBool("isRunning", false);
        StartCoroutine("Hurt");
    }
    IEnumerator Hurt()
    {
        animator.SetTrigger("Damage");
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<EnemyMovement>().SendMessage("setMoving", true);
        canAttack = true;
    }
}
