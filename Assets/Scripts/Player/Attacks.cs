using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    //-----------------------------------------------------Combo attack-------------------------------------------------------
    string[] combo = { "Combo_Attack_1", "Combo_Attack_2" };
    private int comboIndex = 0;
    private float nextAttackTime = 0;
    private float lastAttackTime = 0;
    private float comboDelay = 0.5f;
    //--------------------------------------------Jump down attack-------------------------------
    public Vector2 JumpAttackBoxPosition;
    public Vector2 JumpAttackBox;

    //------------------------ Parry ------------------------------------------------

    //----------------------------------------Attack radius and raycasts-----------------------
    public float basic_attack_radius = 0.5f;
    public float parry_radius = 10.5f;

    [SerializeField] RaycastHit2D[] comboAttackRayCast;

    //----------------------------------Detect Layermask/self Rigidbody/Effect--------------------------------------
    public LayerMask enemies;
    public LayerMask bosses;
    private Rigidbody2D rb;
    public Animator animator;
    private PlayerStat playerstat;
    private PlayerController playermovement;

    //---------------------------------Action check boolean-----------------------------------------
    private bool canBeDamaged;
    public bool canMove;

    //------------------------------------------Rage mode------------------
    private bool RageMode;
    private bool canAttack;


    void Awake()
    {
        canAttack = true;
        canBeDamaged = true;
        basic_attack_radius = 0.23f;
        JumpAttackBox = new Vector2(0.11f, 0.30f);
        enemies = LayerMask.GetMask("Enemy");
        bosses = LayerMask.GetMask("Boss");
        playerstat = gameObject.GetComponent<PlayerStat>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        playermovement = gameObject.GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        //RaycastDebugger
        RayCastDebugger();
        if (canAttack)
        {
            //Combo
            Combo();
            //Jump down attack
            JumpAttack();
            //Parry
        }
    }




    //---------------------------------Combo------------------------
    void Combo()
    {
        if (Input.GetKeyDown(KeyCode.K) && playermovement.isGrounded)
        {
            if (Time.time - lastAttackTime > comboDelay)
            {
                comboIndex = 0;

            }
            if (Time.time > nextAttackTime)
            {
                animator.Play(combo[comboIndex]);
                comboIndex += 1;

                if (comboIndex >= combo.Length)
                {
                    comboIndex = 0;
                }
                lastAttackTime = Time.time;
                nextAttackTime = Time.time + 0.3f;
            }
        }
    }

    void ComboAttack(float damage)
    {

        // //Detect enemies in range of the attack
        if (transform.localScale.x > 0)
        {
            comboAttackRayCast = Physics2D.RaycastAll(transform.position, Vector2.right, basic_attack_radius, enemies);
        }
        else
        {
            comboAttackRayCast = Physics2D.RaycastAll(transform.position, Vector2.left, basic_attack_radius, enemies);
        }
        foreach (RaycastHit2D enemy in comboAttackRayCast)
        {
            if (enemy.collider != null)
            {
                enemy.collider.GetComponent<EnemyStat>().SendMessage("decreaseHP", playerstat.damage);
                Debug.Log(enemy.collider.GetComponent<EnemyStat>().currentHP);
            }
        }
    }
    void JumpAttack()
    {
        if (Input.GetKey(KeyCode.L) && !playermovement.isGrounded)
        {
            animator.Play("Jump_Down_Attack");
        }
    }
    void JumpAttackFrame(float damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y - 0.1f), JumpAttackBox, enemies);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Enemy hit");
        }
    }

    //----------------------------------------- Parry------------------------------------------------------

    void Parry()
    {
        // gameObject.GetComponent<PlayerController>().SendMessage("setMoving", true);
        // //Get user parry time
        // if (transform.localScale.x < 0)
        // {
        //     gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-30, 0);
        // }
        // else
        // {
        //     gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(30, 0);
        // }
        // Debug.Log(hitEnemies.Length);
        // try
        // {
        //     foreach (Collider2D enemy in hitEnemies)
        //     {
        //         try
        //         {
        //             // enemy.transform.GetComponent<EnemyCombat>().SendMessage("SetPlayerAttackTime", userDetail);
        //         }
        //         catch { }

        //     }
        // }
        // catch
        // {
        //     Debug.Log("Exception NUll for player:");
        // }
        // try
        // {
        //     foreach (Collider2D boss in hitBoss)
        //     {
        //         try
        //         {
        //             // boss.transform.GetComponent<BossCombat>().SendMessage("SetPlayerAttackTime", userDetail);
        //         }
        //         catch { }
        //     }
        // }
        // catch
        // {
        //     Debug.Log("Exception NUll for player:");
        // }
        // gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        // canAttack = true;

    }
    //-----------------------------------------------------------------Rage mode--------------------------------------------------------

    // public void EnterRageMode()
    // {
    //     playerstat.SendMessage("decreaseHP", playerstat.currentHP * 60 / 100);
    //     weaponHolder.GetComponent<WeaponHolder>().SendMessage("setRage", true);
    //     RageParticle.Play();
    //     attackDamage += attackDamage * 0.5f;
    //     StartCoroutine(DecreaseRage());

    // }
    // IEnumerator DecreaseRage()
    // {
    //     yield return new WaitForSeconds(10.0f);
    //     weaponHolder.GetComponent<WeaponHolder>().SendMessage("setRage", false);
    //     playerstat.GetComponent<PlayerStat>().SendMessage("resetRage");
    //     attackDamage -= attackDamage * 0.5f;
    // }
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

    //-------------------------------RayCastDebugger
    void RayCastDebugger()
    {
        //---------------------------attack raycast-------------------
        if (transform.localScale.x > 0)
        {
            Debug.DrawRay(transform.position, Vector2.right * basic_attack_radius, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.left * basic_attack_radius, Color.red);
        }

    }
    //------------------------------------Gizmos------------------------------
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y - 0.1f), JumpAttackBox);
    }
}
