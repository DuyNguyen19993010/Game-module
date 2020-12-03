using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBow : MonoBehaviour
{
    private float speed = 1f;   //speed of the projectile
    public Rigidbody2D rb;
    private PlayerStat stats;  //get the playerstat script

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        stats = GameObject.Find("Player").GetComponent<PlayerStat>();
        rb.velocity = transform.right * speed; //shoot the arrow with speed
    }

    // colliderTrigger checks if the arrow hit the player
    void OnTriggerEnter2D(Collider2D collides)
    {
        PlayerController player = collides.GetComponent<PlayerController>();
        //if hit the player
        if (player != null)
        {
            stats.SendMessage("decreaseHP", 1); //hurt player
            destroyProjectile();    //destroy the object after hitted
        }
        else
        {
            Invoke("destroyProjectile", 2f); //if it does not hit anything, destroy the object after 2s.
        }
    }

    //destroy the object
    void destroyProjectile()
    {
        Destroy(gameObject);
    }
}
