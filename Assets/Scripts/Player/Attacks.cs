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
    // public float comboDelay;
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

    //-------------------------------------Ally skill cooldown------------
    [Header("Ally skill cooldown")]
    public float shiroinu_cooldown;
    public float shiroinu_nextUseTime;
    public float kuroinu_cooldown;
    public float kuroinu_nextUseTime;
    //------------------------Ally prefab ----------------------------
    public GameObject shiroinuPrefab;
    public GameObject kuroinuPrefab;
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


    void Start()
    {
        //-------------------------Set up for enemy layers
        enemies = LayerMask.GetMask("Enemy");
        bosses = LayerMask.GetMask("Boss");
        playerstat = gameObject.GetComponent<PlayerStat>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        playermovement = gameObject.GetComponent<PlayerController>();
        shiroinuPrefab = (GameObject)Resources.Load("Prefab/Character/Ally/Shiroinu", typeof(GameObject));
        kuroinuPrefab = (GameObject)Resources.Load("Prefab/Character/Ally/Kuroinu", typeof(GameObject));
        //------------------------Player attack stat setup-----------------
        canAttack = true;
        canBeDamaged = true;
        basic_attack_radius = 0.23f;
        isSpining = false;
        // ---------------------Skill Next use time----------------
        fire_skill_nextUseTime = 0;
        moon_skill_nextUseTime = 0;
        // ---------------------Ally Next use time----------------
        shiroinu_nextUseTime = 0;
        kuroinu_nextUseTime = 0;
        // ---------------------Skill cooldown----------------
        fire_skill_cooldown = 3;
        moon_skill_cooldown = 10;
        // ---------------------Ally cooldown----------------
        shiroinu_cooldown = 20;
        kuroinu_cooldown = 20;
        JumpAttackBox = new Vector2(0.11f, 0.30f);
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
            //Shiroinu Skill
            if (playerstat.player.skills.GetSkillList()[2].enabled)
            {
                Call_Shiroinu();
            }
            //Kuroinu Skill
            if (playerstat.player.skills.GetSkillList()[3].enabled)
            {
                Call_Kuroinu();
            }

            //-------------------Unlock skills--------------------------------


        }
    }
    public void UnlockSkill(int index)
    {
        playerstat.player.skills.GetSkillList()[index].enabled = true;
        GameObject.Find("Skill_Cooldown_Container").GetComponent<Skill_UI>().refresh = true;
    }




    //---------------------------------Combo------------------------
    void Combo()
    {
        if (!Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.K) && playermovement.isGrounded)
        {
            if (Time.time - lastAttackTime > playerstat.player.attackDelay + 0.2)
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
                nextAttackTime = Time.time + playerstat.player.attackDelay;
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
                try
                {
                    enemy.collider.GetComponent<EnemyStat>().SendMessage("decreaseHP", playerstat.damage);
                }
                catch { }
                playerstat.increaseRage(3);
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
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("Moon_Skill_Spin", false);
        }

    }
    public void SpinAttack()
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
    void OnCollisionEnter2D(Collision2D col)
    {
    }


    void UsingMoonSKill()
    {
        isUsingMoonSkill = true;
    }
    void NotUsingMoonSkill()
    {
        isUsingMoonSkill = false;
    }


    //Shiroinu Skill
    void Call_Shiroinu()
    {
        if (Input.GetKeyDown(KeyCode.C) && Time.time > shiroinu_nextUseTime)
        {
            Debug.Log("Shiroinu Called");
            GameObject shiroinu = Instantiate(shiroinuPrefab);
            shiroinu.transform.position = transform.position;
            SetShiroinuCoolDown();
        }

    }


    //Kuroinu Skill
    void Call_Kuroinu()
    {
        if (Input.GetKeyDown(KeyCode.V) && Time.time > kuroinu_nextUseTime)
        {
            Debug.Log("Kuroinu Called");
            GameObject kuroinu = Instantiate(kuroinuPrefab);
            kuroinu.transform.position = transform.position;
            SetKuroinuCoolDown();
        }


    }



    //------------------------------------Set cooldown for each skill
    void SetFireCoolDown()
    {
        animator.ResetTrigger("Fire_skill");
        fire_skill_nextUseTime = Time.time + fire_skill_cooldown;
    }

    void SetMoonCoolDown()
    {
        moon_skill_nextUseTime = Time.time + moon_skill_cooldown;
    }
    void SetShiroinuCoolDown()
    {
        shiroinu_nextUseTime = Time.time + shiroinu_cooldown;
    }

    void SetKuroinuCoolDown()
    {
        kuroinu_nextUseTime = Time.time + kuroinu_cooldown;
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
