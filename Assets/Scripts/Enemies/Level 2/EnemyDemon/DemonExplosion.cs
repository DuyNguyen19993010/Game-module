using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonExplosion : MonoBehaviour
{
    //Animator of the Demon
    private Animator animator;
    //Enemymovement script
    private EnemyMovement movement;
    //get the playerstat
    private PlayerStat stats;
    //------Player layer-------
    public LayerMask playerLayer;
    //Explode radius
    [SerializeField]
    private float explodeRadius;
    [Header("Particle effects")]
    public GameObject explosionEffectLayer1;
    public GameObject explosionEffectLayer2;
    public GameObject explosionEffectLayer3;
    public GameObject explosionEffectLayer4;



    void Start()
    {
        //initialiszing the animator, movement, playerstat
        animator = gameObject.GetComponent<Animator>();
        movement = gameObject.GetComponent<EnemyMovement>();
        stats = GameObject.Find("Player").GetComponent<PlayerStat>();
        //----------------------Setting explode radius------------------------------------------
        explodeRadius = 0.5f;
    }

    void Update()
    {
        //disable the movement of demon and trigger the explosion animation
        //if the enemy is in attack range(which means detected player and ready for attacking)
        if (movement.isInAttackRange && movement.followingPlayer)
        {
            animator.SetBool("isRunning", false);
            //disable the moves
            movement.SendMessage("FreezeEnemy");
            //trigger the explosion animation
            animator.SetTrigger("explodes");
        }
        //else keep the demon moving around
        else
        {
            animator.SetBool("isRunning", true);
        }

    }
    //-----------------------------this is getting called through the animation event----------------------------
    void explosionDamage()
    {
        try
        {
            Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, explodeRadius, playerLayer);
            hitPlayer.gameObject.GetComponent<PlayerStat>().SendMessage("decreaseHP", stats.damage);
        }
        catch
        {
            Debug.Log("Player not found");

        }
    }
    //-------------------------------------------------------------------------------------------------------------
    //-----------------------------this is getting called through the animation event at the end to destroy the demon enemy----------------------------
    void destroySelf()
    {
        Destroy(gameObject);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
    public void explosionEffectLayer1Play()
    {
        Instantiate(explosionEffectLayer1, transform.position, transform.rotation);
    }
    public void explosionEffectLayer2Play()
    {
        Instantiate(explosionEffectLayer2, transform.position, transform.rotation);
    }
    public void explosionEffectLayer3Play()
    {
        Instantiate(explosionEffectLayer3, transform.position, transform.rotation);
    }
    public void explosionEffectLayer4Play()
    {
        Instantiate(explosionEffectLayer4, transform.position, transform.rotation);
    }
}
