using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBall : MonoBehaviour
{
    private float speed = 1f;   //speed of the projectile
    public Rigidbody2D rb;
    private PlayerStat stats;  //get the playerstat script
    private Animator animator;  //animator of the shoted ball projectile

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        try
        {
            stats = GameObject.Find("Player").GetComponent<PlayerStat>();
        }
        catch { }
        rb.velocity = transform.right * speed; //shoot the ball with speed
        animator = gameObject.GetComponent<Animator>();
    }

    //once the projectile hitted the player object
    //the animation of projectile starts
    //decrease the player's hp
    //and destroy the projectile object
    void OnTriggerEnter2D(Collider2D collides)
    {
        try
        {
            PlayerController player = collides.GetComponent<PlayerController>();
            if (player != null)
            {
                animator.SetTrigger("hitted");
                stats.SendMessage("decreaseHP", 1);
                destroyProjectile();
            }
            else
            {
                Invoke("destroyProjectile", 2f); //if it does not hit anything, destroy the object after 2s.
            }
        }
        catch { }

    }

    //destroy the object
    void destroyProjectile()
    {
        Destroy(gameObject);
    }
}
