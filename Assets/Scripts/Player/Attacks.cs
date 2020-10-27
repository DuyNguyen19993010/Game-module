using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public Animator animator;
    public Transform attackBox;
    public float basic_attack_Range = 0.5f;
    public LayerMask enemies;
    private Rigidbody2D rb;
    public bool canMove;
    public float attackTime = 1f;
    public float lightAttackTimeToWait;
    float nextAttackTime = 0f;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        lightAttackTimeToWait = 0.04f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.K) && gameObject.GetComponent<PlayerController>().isGrounded && gameObject.GetComponent<PlayerController>().rb.velocity.x == 0.0f)
            {
                gameObject.GetComponent<PlayerController>().SendMessage("setMoving", false);
                StartCoroutine(BasicAttack());
                nextAttackTime = Time.time + lightAttackTimeToWait / attackTime;
            }
        }


    }
    public IEnumerator BasicAttack()
    {
        rb.velocity = new Vector2(0, 0);
        //Play the attack animation
        animator.SetTrigger("light_attack");
        //Detect enemies in range of the attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackBox.position, basic_attack_Range, enemies);
        yield return new WaitForSeconds(lightAttackTimeToWait);
        //Deal damage to those enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            float[] AttackDetails = { 10, transform.position.x };
            enemy.transform.parent.SendMessage("Damage", AttackDetails);
            enemy.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 10));

        }
        gameObject.GetComponent<PlayerController>().SendMessage("setMoving", true);


    }
    void OnDrawGizmosSelected()
    {
        if (attackBox == null)
        {
            Gizmos.DrawWireSphere(attackBox.position, basic_attack_Range);
        }
    }
}
