﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    // animator
    public Animator animator;
    //---------------------------------Enemy stat
    private EnemyStat enemyStat;
    private EnemyMovement enemyMovement;
    private Rigidbody2D rb;
    //-------------------------------Used to decide if the enemy can attack or not---------------
    private bool canAttack;
    //------------------------------Limit number of attack in 1s------------------------------
    private float nextAttackTime;
    //------------------------------- used for calculating parry----------------------
    public float damage;
    private float enemyAttackTime;
    private float userAttackTime;
    private float playerDamage;
    //-----------------------------Attack raycast------------------------------
    public float attackRadius;
    [SerializeField] RaycastHit2D attackRayCast;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Awake()
    {
        damage = 10.0f;
        attackRadius = 0.2f;
        canAttack = true;
        nextAttackTime = 0;
        enemyAttackTime = 0;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        enemyStat = gameObject.GetComponent<EnemyStat>();
        enemyMovement = gameObject.GetComponent<EnemyMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        RayCastDebugger();
        if (canAttack)
        {
            if (Time.time > nextAttackTime && !enemyMovement.isMoving)
            {
                CloseRangeAttack();
                nextAttackTime = Time.time + 0.4f;
            }
        }

    }
    void CloseRangeAttack()
    {
        //Call attack animation
        Attack();
    }
    //----------------------------Attack function called using animation event------------------------
    void Attack()
    {
        enemyAttackTime = Time.time;
        // Debug.Log("Enemy attacking at time: " + enemyAttackTime);
        // Debug.Log("Player attacking at time: " + userAttackTime);
        //Check if the player parried at the right time
        if (userAttackTime < enemyAttackTime && userAttackTime > enemyAttackTime - 0.25 && userAttackTime != 0)
        {
            Debug.Log("Enemy Parried");
            // try { target.GetComponent<Attacks>().SendMessage("successParry"); } catch { Debug.Log("Exception NUll for enemy:"); }
        }
        else
        {
            //Hurt player
            if (transform.localScale.x > 0)
            {
                attackRayCast = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.01f), Vector2.right, attackRadius, playerLayer);
            }
            else
            {
                attackRayCast = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.01f), Vector2.left, attackRadius, playerLayer);
            }
            if (attackRayCast.collider != null)
            {
                //Attack player
                attackRayCast.collider.gameObject.GetComponent<PlayerStat>().SendMessage("decreaseHP", damage);
            }
        }
        enemyAttackTime = 0;
        userAttackTime = 0;
    }
    //-----------------------Used to hurt enemy----------------------
    void Hurt(float damage)
    {
        try { enemyStat.SendMessage("decreaseHP", damage); } catch { Debug.Log("Exception NUll for enemy:"); }
        try { enemyMovement.SendMessage("setMoving", true); } catch { Debug.Log("Exception NUll for enemy:"); }
        canAttack = true;
    }
    //-------------------------------------Used to get player parry time----------------------------
    void SetPlayerAttackTime(float[] playerDetail)
    {
        userAttackTime = playerDetail[0];
        playerDamage = playerDetail[1] * 3;
    }
    //------------------------------------Raycast debugger------------------------------------
    void RayCastDebugger()
    {
        if (transform.localScale.x > 0)
        {
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.01f), Vector2.right * attackRadius, Color.blue);
        }
        else
        {
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.01f), Vector2.left * attackRadius, Color.blue);
        }
    }
}