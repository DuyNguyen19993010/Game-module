using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    //-----------------------------------------------------Combo attack-------------------------------------------------------
    [Header("Combo list and range")]
    string[] combo = { "Combo_Attack_1", "Combo_Attack_2" };
    private int comboIndex = 0;
    private float nextAttackTime = 0;
    private float lastAttackTime = 0;
    private float comboDelay = 0.5f;
    public float basic_attack_radius = 0.5f;
    //--------------------------------------------Jump down attack-------------------------------
    [Header("Jump attack hit box's size and position")]
    public Vector2 JumpAttackBoxPosition;
    public Vector2 JumpAttackBox;

    //------------------------ Parry ------------------------------------------------

    //----------------------------------------Attack radius and raycasts-----------------------
    [Header("Parry attack radius")]
    public float parry_radius = 10.5f;

    [SerializeField] RaycastHit2D[] comboAttackRayCast;
    //---------------------------Skills-------------------------------------
    [Header("Fire SKill")]
    //Fire Skill
    public bool isUsingFireSkill;
    public float fire_skill_nextUseTime;
    public float fire_skill_cooldown;

    public GameObject fireSlash;
    [Header("Moon SKill")]
    //Moon Skill
    public bool isUsingMoonSkill;
    public float moon_skill_nextUseTime;
    public float moon_skill_cooldown;
    private bool isSpining;

    [Header("Wind SKill")]
    //Wind Skill
    public bool isUsingWindSkill;
    public float wind_skill_nextUseTime;
    public float wind_skill_cooldown;
    private bool isDiving;
    public float slamRadius;
    public float diveForce;

    //----------------------------------Detect Layermask/self Rigidbody/Effect--------------------------------------
    [Header("Enemy layers")]
    public LayerMask enemies;
    public LayerMask bosses;
    private Rigidbody2D rb;
    [Header("Animator")]
    public Animator animator;
    private PlayerStat playerstat;
    private PlayerController playermovement;

    //---------------------------------Action check boolean-----------------------------------------
    [Header("Check if can be damaged or moved")]
    public bool canBeDamaged;
    public bool canMove;


    //------------------------------------------Rage mode------------------
    private bool RageMode;
    private bool canAttack;


    void Awake()
    {
        canAttack = true;
        canBeDamaged = true;
        basic_attack_radius = 0.23f;
        fire_skill_nextUseTime = 0;
        wind_skill_nextUseTime = 0;
        slamRadius = 0.4f;
        diveForce = 30;
        moon_skill_nextUseTime = 0;
        isSpining = false;
        fire_skill_cooldown = 3;
        wind_skill_cooldown = 3;
        moon_skill_cooldown = 3;
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

            //---------------------------Skills-------------------------------------
            //Fire Skill
            if (playerstat.player.skills.GetSkillList()[0].enabled)
            {
                FireSkill();
            }
            //Moon Skill
            if (playerstat.player.skills.GetSkillList()[1].enabled)
            {
                StartCoroutine(MoonSkill());
            }
            //Wind Skill
            if (playerstat.player.skills.GetSkillList()[2].enabled)
            {
                WindSkill();
            }

            //-------------------Unlock skills--------------------------------

        }
    }
    void UnlockSkill(int index)
    {
        playerstat.player.skills.GetSkillList()[index].enabled = true;
        GameObject.Find("Skill_Cooldown_Container").GetComponent<Skill_UI>().refresh = true;
    }




    //---------------------------------Combo------------------------
    void Combo()
    {
        if (!Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.K) && playermovement.isGrounded)
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
                playerstat.SendMessage("increaseRage", 1);
                ShakeCamera();
                Debug.Log(enemy.collider.GetComponent<EnemyStat>().currentHP);
            }
        }
    }

    //-----------------------------Jump Attack
    void JumpAttack()
    {
        if (Input.GetKeyDown(KeyCode.L) && !playermovement.isGrounded)
        {
            animator.Play("Jump_Down_Attack");
        }
    }
    void JumpAttackFrame()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y - 0.1f), 0.05f, enemies);
        Debug.Log(hitEnemies.Length + " enemies dected");
        try
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyStat>().SendMessage("decreaseHP", playerstat.damage);
            }
        }
        catch
        {
            Debug.Log("Null");
        }
    }


    //---------------------------Skills-------------------------------------
    //Fire Skill
    void FireSkill()
    {
        if (playermovement.isGrounded && Input.GetKey(KeyCode.S))
        {
            if (Input.GetKeyDown(KeyCode.K) && Time.time > fire_skill_nextUseTime)
            {
                Debug.Log("Fire called");
                animator.SetTrigger("Fire_skill");
            }
        }
    }
    void SpawnFireSlash()
    {
        GameObject slashes = Instantiate(fireSlash) as GameObject;
        if (transform.localScale.x > 0)
        {
            slashes.transform.position = new Vector2(transform.position.x + 0.4f, transform.position.y);

        }
        else
        {
            slashes.transform.position = new Vector2(transform.position.x - 0.4f, transform.position.y);
        }
    }
    void UsingFireSKill()
    {
        isUsingFireSkill = true;
    }
    void NotUsingFireSkill()
    {
        isUsingFireSkill = false;
    }
    //Moon Skill
    IEnumerator MoonSkill()
    {
        if (Input.GetKeyDown(KeyCode.L) && playermovement.isGrounded && Time.time > moon_skill_nextUseTime)
        {
            isSpining = true;
            animator.SetBool("Moon_Skill_Spin", true);
        }
        if (isSpining)
        {
            playerstat.canBeHurt = false;

            yield return new WaitForSeconds(5.0f);
            isSpining = false;
            playerstat.canBeHurt = true;
            UsingMoonSKill();
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("Moon_Skill_Spin", false);
            NotUsingMoonSkill();
        }

    }
    IEnumerator SpinAttack()
    {
        while (isSpining)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 0.1f, enemies);
            try
            {
                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<EnemyStat>().SendMessage("decreaseHP", playerstat.damage / 3);
                    ShakeCamera();
                }
            }
            catch
            {
                Debug.Log("Null");
            }
        }
        yield return new WaitForSeconds(0f);
    }


    void UsingMoonSKill()
    {
        isUsingMoonSkill = true;
    }
    void NotUsingMoonSkill()
    {
        isUsingMoonSkill = false;
    }


    //Wind Skill
    void WindSkill()
    {
        // if (Input.GetKeyDown(KeyCode.K) && !playermovement.isGrounded && Time.time > wind_skill_nextUseTime)
        // {
        //     animator.SetTrigger("Wind_skill");
        // }
        // if (isDiving)
        // {
        //     Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(transform.position, slamRadius, enemies);
        //     foreach (Collider2D enemy in hitEnemy)
        //     {
        //         enemy.GetComponent<EnemyStat>().SendMessage("decreaseHP", playerstat.damage);
        //     }
        // }

    }
    void Diving()
    {
        isDiving = true;
    }
    void NotDiving()
    {
        isDiving = false;
    }
    void freezePlayerInMidAir()
    {
        rb.gravityScale = 0;
    }
    void unFreezePlayerInMidAir()
    {
        rb.gravityScale = 1;
    }

    void UsingWindSKill()
    {
        isUsingWindSkill = true;
    }
    void NotUsingWindSkill()
    {
        isUsingWindSkill = false;
    }


    //------------------------------------Set cooldown for each skill
    void SetFireCoolDown()
    {
        animator.ResetTrigger("Fire_skill");
        fire_skill_nextUseTime = Time.time + fire_skill_cooldown;
    }
    void Dive()
    {
        rb.AddForce(new Vector2(0, diveForce));
    }
    void SetWindCoolDown()
    {
        wind_skill_nextUseTime = Time.time + wind_skill_cooldown;
    }
    void SetMoonCoolDown()
    {
        moon_skill_nextUseTime = Time.time + moon_skill_cooldown;
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
    void enableAttack()
    {
        canAttack = true;
    }
    void disableAttack()
    {
        canAttack = false;
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
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y - 0.1f), 0.05f);
        if (isDiving)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, slamRadius);
        }
        if (isSpining)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.1f);
        }
    }
    //Shake camera
    void ShakeCamera()
    {
        CameraShake.Instance.ShakeCamera(0.2f, .1f);
    }
}
