using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiroinu : MonoBehaviour
{
    //-----------------------------Raycast used to detect and decide on which enemy to attack------------------
    private RaycastHit2D eyeSightRayCast;
    //---------------------Used to check for player facing direction-----------
    private Transform player;
    //------------------------The ally rigibody----------
    private Rigidbody2D rb;
    //Ally move speed
    [Header("Movement speed")]
    public float speed;
    //--------------------------------used to check if the ally is running and he can only perform his dive special attack when he is running--------------
    public bool isRunning;
    //-----------------------Raycast to detect enemy to attaack---------
    [Header("Detect radius")]
    public LayerMask whoToAttack;
    private RaycastHit2D[] attackRayCast;
    public float attackRadius;
    //Used to decide where to dive to
    public Vector2 divePoint;
    public bool isDiving;
    private int diveDirection;
    //-----------------------------Fire Pillar----------------------
    GameObject firePillar;
    //---------------------------used to calculate when to disappear if not perform attack for too long------------
    private float duration;
    private float disapear;

    //---------------------Used to calculate when to disapear after perform dive attack
    private float disapearTime;
    private float diveTime;
    public bool explode;



    void Start()
    {
        firePillar = (GameObject)Resources.Load("Prefab/Character/Player/Fire_Pillar", typeof(GameObject));
        isRunning = false;
        attackRadius = 0.4f;
        isDiving = false;
        explode = false;
        player = GameObject.Find("Player").transform;
        speed = 0.5f;
        diveTime = 0.2f;
        duration = 5;
        disapear = Time.time + duration;
        rb = transform.gameObject.GetComponent<Rigidbody2D>();
        if (player.transform.localScale.x > 0)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
    }
    void Update()
    {
        if (Time.time > duration)
        {
            Destroy(transform.gameObject);

        }
        RayCastDebugger();
        if (rb.velocity.x != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        //----------------------Detect enemy and if found , perform a dive attack------------------------
        if (!isDiving)
        {



            detectEnemy();
        }
        else
        {
            DealDiveDamage();
        }

        Flip();
    }
    void Flip()
    {
        if (rb.transform.localScale.x > 0 && rb.velocity.x < 0)
        {
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        else if (rb.transform.localScale.x < 0 && rb.velocity.x > 0)
        {
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

    }
    void detectEnemy()
    {
        if (rb.transform.localScale.x > 0)
        {
            attackRayCast = Physics2D.RaycastAll(transform.position, Vector2.right, attackRadius, whoToAttack);
            if (attackRayCast.Length > 0)
            {
                //Get the last enemy detected
                RaycastHit2D lastEnemy = attackRayCast[attackRayCast.Length - 1];
                divePoint = lastEnemy.transform.position;
                Dive(1);
            }
        }
        else
        {
            attackRayCast = Physics2D.RaycastAll(transform.position, Vector2.left, attackRadius, whoToAttack);
            if (attackRayCast.Length > 0)
            {
                //Get the last enemy detected
                RaycastHit2D lastEnemy = attackRayCast[attackRayCast.Length - 1];
                divePoint = lastEnemy.transform.position;
                Dive(-1);
            }
        }

    }
    void Dive(int direction)
    {
        disapearTime = Time.time + diveTime;
        diveDirection = direction;
        isDiving = true;

    }
    void DealDiveDamage()
    {
        float posY = (transform.position.y) - 0.02f - transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        GameObject spawnedPillar = Instantiate(firePillar, new Vector3(transform.position.x, posY), Quaternion.identity) as GameObject;
        rb.AddForce(new Vector2(25 * diveDirection, 0));
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 0.2f, whoToAttack);
        foreach (Collider2D hitEnemy in hitEnemies)
        {
            hitEnemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(400 * diveDirection, 0));
            if (Time.time > disapearTime)
            {
                hitEnemy.GetComponent<EnemyStat>().decreaseHP(100.0f);
            }
        }
        if (Time.time > disapearTime)
        {
            explode = true;
        }
    }
    void RayCastDebugger()
    {
        if (rb.transform.localScale.x > 0)
        {
            Debug.DrawRay(transform.position, Vector2.right * attackRadius, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.left * attackRadius, Color.red);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.2f);

    }


}
