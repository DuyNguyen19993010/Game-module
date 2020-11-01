using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    //----------------------------------------Attack radius-----------------------
    public float basic_attack_Range = 0.5f;
    public float parry_attack_Range = 10.5f;
    //----------------------------------Detect Layermask/self Rigidbody/Effect--------------------------------------
    public LayerMask enemies;
    public LayerMask boss;
    private Rigidbody2D rb;
    public ParticleSystem RageParticle;
    public Animator animator;
    public Transform attackBox;
    private PlayerStat playerstat;

    //------------------------------------------K Combo Variable-----------------
    public float attackTime = 1f;
    public float lightAttackTimeToWait = 0.04f;

    //NextAttackTime<combo_startTime
    private float nextAttackTime = 0.0f;//Calculate realtime time to able to press button again
    private float comboDelay = 1.2f;//time limit between each attack in the combo
    public float keyPressedCount = 0.0f;//Time that K is pressed
    private float userLastClicked = 0;
    //---------------------------------Action check boolean-----------------------------------------
    private bool canBeDamaged;
    public bool canMove;
    //------------------------ Parry ------------------------------------------------
    private float minimumHoldTime;
    private float userHoldTime;
    private float userReleaseTime;

    //------------------------------------------Rage mode------------------
    public GameObject weaponHolder;
    private bool RageMode;
    private bool canAttack;

    public float attackDamage;


    void Start()
    {
        attackDamage = 10.0f;
        enemies = LayerMask.GetMask("Enemy");
        boss = LayerMask.GetMask("Boss");
        canAttack = true;
        playerstat = gameObject.GetComponent<PlayerStat>();
        userHoldTime = 0.0f;
        minimumHoldTime = 2.0f;
        canBeDamaged = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        //------------------------------K combo
        // if (Input.GetKeyDown(KeyCode.K) && gameObject.GetComponent<PlayerController>().isGrounded && gameObject.GetComponent<PlayerController>().rb.velocity == new Vector2(0, 0))
        // {
        //     rb.velocity = new Vector2(0, 0);
        //     gameObject.GetComponent<PlayerController>().SendMessage("setMoving", false);
        //     StartCoroutine(BasicAttack());
        // }

        if (gameObject.GetComponent<Rigidbody2D>().velocity.x == 0)
        {
            canAttack = true;

        }
        else
        {
            canAttack = false;
        }

        if (Time.time - userLastClicked > comboDelay)
        {
            keyPressedCount = 0;
        }
        if (Input.GetKeyDown(KeyCode.K) && canAttack && gameObject.GetComponent<PlayerController>().isGrounded && gameObject.GetComponent<PlayerController>().rb.velocity == new Vector2(0, 0))
        {

            rb.velocity = new Vector2(0, 0);
            userLastClicked = Time.time;
            keyPressedCount++;
            if (keyPressedCount == 1)
            {
                animator.SetBool("LightAttack1", true);
                StartCoroutine(BasicAttack());
            }
            keyPressedCount = Mathf.Clamp(keyPressedCount, 0, 2);
        }




        if (Time.time >= nextAttackTime && canAttack)
        {
            //----------------------Hold and release P for parry
            if (Input.GetKey(KeyCode.P) && gameObject.GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0))
            {
                gameObject.GetComponent<PlayerController>().SendMessage("setMoving", false);
                rb.velocity = new Vector2(0, 0);

                Hold();
            }
            if (Input.GetKeyUp(KeyCode.P))
            {
                gameObject.GetComponent<PlayerController>().SendMessage("setMoving", false);
                rb.velocity = new Vector2(0, 0);
                StartCoroutine(Parry());
                userHoldTime = 0;
                nextAttackTime = Time.time + lightAttackTimeToWait / attackTime;
            }
            // //----------------------Press R when rage is full to go into rage mode
            if (Input.GetKeyDown(KeyCode.R) && playerstat.Rage >= 100)
            {
                gameObject.GetComponent<PlayerController>().SendMessage("setMoving", false);
                rb.velocity = new Vector2(0, 0);

                Debug.Log("RageMode ON !!!!!!!!!!!!");
                EnterRageMode();
                RageMode = true;
                nextAttackTime = Time.time + lightAttackTimeToWait / attackTime;
            }

        }



    }




    //---------------------------------Combo------------------------
    void Attack1()
    {
        if (keyPressedCount >= 2)
        {

            animator.SetBool("LightAttack2", true);
            StartCoroutine(BasicAttack());
        }
        else
        {
            animator.SetBool("LightAttack1", false);
            keyPressedCount = 0;
        }
    }

    void Attack2()
    {
        animator.SetBool("LightAttack1", false);
        animator.SetBool("LightAttack2", false);
        keyPressedCount = 0;
    }





    public IEnumerator BasicAttack()
    {
        if (RageMode)
        {
            playerstat.SendMessage("increaseHP", playerstat.maxHP * 0.05);

        }
        float[] userDetail = { userHoldTime, attackDamage };
        // //Detect enemies in range of the attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackBox.position, basic_attack_Range, enemies);
        Collider2D[] hitBoss = Physics2D.OverlapCircleAll(attackBox.position, basic_attack_Range, boss);



        // //Delay player attack and force user to stop moving while the attack is happening
        yield return new WaitForSeconds(lightAttackTimeToWait);



        // //Deal damage to those enemies and bosses
        try
        {
            foreach (Collider2D boss in hitBoss)
            {
                try
                {
                    float[] AttackDetails = { 10, transform.position.x };
                    boss.transform.GetComponent<BossCombat>().SendMessage("Damage", attackDamage);
                    playerstat.SendMessage("increaseRage", 10);
                }
                catch { }
            }
        }
        catch
        {
            Debug.Log("Exception NUll for player:");
        }
        try
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                try
                {
                    float[] AttackDetails = { 10, transform.position.x };
                    enemy.transform.GetComponent<EnemyCombat>().SendMessage("Damage", attackDamage);
                    playerstat.SendMessage("increaseRage", 10);
                }
                catch { }
            }
        }
        catch
        {
            Debug.Log("Exception NUll for player:");
        }
        gameObject.GetComponent<PlayerController>().SendMessage("setMoving", true);

    }
    //-----------------------------------------------------------------Rage mode--------------------------------------------------------

    public void EnterRageMode()
    {
        playerstat.SendMessage("decreaseHP", playerstat.currentHP * 60 / 100);
        weaponHolder.GetComponent<WeaponHolder>().SendMessage("setRage", true);
        RageParticle.Play();
        attackDamage += attackDamage * 0.5f;
        StartCoroutine(DecreaseRage());

    }
    IEnumerator DecreaseRage()
    {
        yield return new WaitForSeconds(10.0f);
        weaponHolder.GetComponent<WeaponHolder>().SendMessage("setRage", false);
        playerstat.GetComponent<PlayerStat>().SendMessage("resetRage");
        attackDamage -= attackDamage * 0.5f;
    }

    //-----------------------------------------Hold and Release to parry------------------------------------------------------
    public void Hold()
    {
        animator.SetBool("Hold", true);
    }
    IEnumerator Parry()
    {
        gameObject.GetComponent<PlayerController>().SendMessage("setMoving", true);
        //Get user parry time
        userHoldTime = Time.time;
        animator.SetBool("Hold", false);
        float[] userDetail = { userHoldTime, attackDamage };

        // Search all enemy in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackBox.position, parry_attack_Range, enemies);
        Collider2D[] hitBoss = Physics2D.OverlapCircleAll(attackBox.position, parry_attack_Range, boss);
        if (transform.localScale.x < 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-30, 0);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(30, 0);
        }
        Debug.Log(hitEnemies.Length);
        try
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                try
                {
                    enemy.transform.GetComponent<EnemyCombat>().SendMessage("SetPlayerAttackTime", userDetail);
                }
                catch { }

            }
        }
        catch
        {
            Debug.Log("Exception NUll for player:");
        }
        try
        {
            foreach (Collider2D boss in hitBoss)
            {
                try
                {
                    boss.transform.GetComponent<BossCombat>().SendMessage("SetPlayerAttackTime", userDetail);
                }
                catch { }
            }
        }
        catch
        {
            Debug.Log("Exception NUll for player:");
        }
        yield return new WaitForSeconds(0.01f);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        canAttack = true;

    }





    //Used to damage the player
    IEnumerator Damage(float damage)
    {
        if (canBeDamaged)
        {
            gameObject.GetComponent<PlayerController>().SendMessage("setMoving", false);

            canBeDamaged = false;
            yield return new WaitForSeconds(1 / 2);
            canBeDamaged = true;
            gameObject.GetComponent<PlayerStat>().SendMessage("decreaseHP", damage);
        }
    }


    void successParry()
    {
        playerstat.SendMessage("increaseRage", 30);
    }
}
