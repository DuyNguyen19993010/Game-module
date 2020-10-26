using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public Animator animator;
    public Transform attackBox;
    public float basic_attack_Range = 0.5f;
    public LayerMask enemies;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Attack Triggered");
            BasicAttack();
        }

    }
    void BasicAttack()
    {
        //Play the attack animation
        animator.SetTrigger("BasicAttack");
        //Detect enemies in range of the attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackBox.position, basic_attack_Range, enemies);
        //Deal damage to those enemies
        foreach (Collider2D enemy in hitEnemies)
        {   
            float[] AttackDetails = {10, transform.position.x};
            enemy.transform.parent.SendMessage("Damage", AttackDetails);
            enemy.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 25)); 
            
        }


    }
    void OnDrawGizmosSelected()
    {
        if (attackBox == null)
        {
            Gizmos.DrawWireSphere(attackBox.position, basic_attack_Range);
        }
    }
}
