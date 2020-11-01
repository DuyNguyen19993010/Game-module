using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
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
    public float userDamage;

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
        try
        {
            gameObject.GetComponent<EnemyMovement>().SendMessage("setMoving", false);
        }
        catch { }
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackFrameSec);
        enemyAttackTime = Time.time;
        Debug.Log("Enemy attacking at time: " + enemyAttackTime);
        Debug.Log("Player attacking at time: " + userAttackTime);
        //&& userAttackTime > enemyAttackTime-0.5
        if (userAttackTime < enemyAttackTime && userAttackTime > enemyAttackTime - 0.25 && userAttackTime != 0)
        {
            Debug.Log("Enemy Parried");
            try { target.GetComponent<Attacks>().SendMessage("successParry"); } catch { Debug.Log("Exception NUll for enemy:"); }
            Damage(userDamage);
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
        try
        {

            gameObject.GetComponent<EnemyMovement>().SendMessage("setMoving", true);

        }
        catch { Debug.Log("Exception NUll for enemy:"); }
    }
    void Damage(float damage)
    {
        Debug.Log("Enemy hurted");
        canAttack = false;
        try { gameObject.GetComponent<EnemyMovement>().SendMessage("setMoving", false); } catch { Debug.Log("Exception NUll for enemy:"); }
        animator.SetBool("isRunning", false);
        StartCoroutine("Hurt", damage);
    }
    IEnumerator Hurt(float damage)
    {
        try { gameObject.GetComponent<EnemyStat>().SendMessage("decreaseHP", damage); } catch { Debug.Log("Exception NUll for enemy:"); }
        yield return new WaitForSeconds(1.5f);
        try { gameObject.GetComponent<EnemyMovement>().SendMessage("setMoving", true); } catch { Debug.Log("Exception NUll for enemy:"); }
        canAttack = true;
    }
    void SetPlayerAttackTime(float[] userDetail)
    {
        userAttackTime = userDetail[0];
        userDamage = userDetail[1] * 3;
    }
}
