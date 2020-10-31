using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    //animator
    private Animator animator;
    //---------------------Stat
    public EnemyMovement enemymovement;
    public float currentHP;
    public float maxHP;
    public float maxHP_temp;
    // Start is called before the first frame update
    void Start()
    {
        maxHP = 100;
        currentHP = maxHP;
        animator = gameObject.GetComponent<Animator>();
        enemymovement = gameObject.GetComponent<EnemyMovement>();
    }
    void Update()
    {

    }

    // Modification for current real time stat
    void increaseHP(float amount)
    {
        currentHP += amount;
    }

    public void decreaseHP(float damage)
    {
        // Play hurt animation
        animator.SetTrigger("Damage");

        //Disable movement for 1/2s 
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        currentHP -= damage;
        if (currentHP <= 0)
        {
            animator.SetTrigger("Die");
            gameObject.SetActive(false);
        }


    }

    // Modification for current permanent stat
    void increaseMaxHP(float amount)
    {
        maxHP_temp = maxHP;
        maxHP += amount;
    }

    void decreaseMaxHP(float amount)
    {
        maxHP_temp = maxHP;
        maxHP += amount;
    }



}
