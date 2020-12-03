using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{

    Rigidbody2D rb;
    private PlayerStat stats;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //initialize the rigidbody and get playerstat
        rb = GetComponent<Rigidbody2D>();
        stats = GameObject.Find("Player").GetComponent<PlayerStat>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //detect the player to see is in the trigger area or not
    //if yes, make the rock falling down
    void OnTriggerEnter2D(Collider2D collides)
    {
        if (collides.gameObject.name.Equals("Player"))
        {
            animator.SetTrigger("Entered");
            rb.isKinematic = false;
        }
    }
    //detect the rock hit the player or not
    //if yes, hurt the player and destroy the rock
    //if not, destroy the rock after a few seconds
    void OnCollisionEnter2D(Collision2D collides)
    {

        if (collides.gameObject.name.Equals("Player"))
        {
            stats.SendMessage("decreaseHP", 1);
            Destroy(gameObject);
        }
        else
        {
            Invoke("DestroySelf", 0.4f);
        }

    }
    //for destroy the rock
    void DestroySelf()
    {

        Destroy(gameObject);

    }
}
