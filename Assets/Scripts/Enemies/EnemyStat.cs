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
        maxHP = 30;
        currentHP = maxHP;
        //Remember to create the animator later after making all animations
        // animator = gameObject.GetComponent<Animator>();
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
    //Used for damaging the enemy
    public void decreaseHP(float damage)
    {
        //--------------------------------Play hurt animation
        // animator.SetTrigger("hurt");
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        currentHP -= damage;
        if (currentHP <= 0)
        {
            //Play death animation
            // animator.SetTrigger("Die");
            Destroy(transform.gameObject);
        }
    }
    //used as animation event to destroy enemy object
    void DeleteEnemy()
    {
        Destroy(gameObject);
    }

}
