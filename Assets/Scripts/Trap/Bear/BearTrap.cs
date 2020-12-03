using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{

    private PlayerStat stats;
    private Animator animator;
    private PlayerController moves;
    // Start is called before the first frame update
    void Start()
    {
        //initialize the rigidbody and get playerstat and get movement from player
        stats = GameObject.Find("Player").GetComponent<PlayerStat>();
        animator = GetComponent<Animator>();
        moves = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //detect the player to see is in the trigger area or not
    //if yes, set the animation start
    void OnTriggerEnter2D(Collider2D collides)
    {
        PlayerController user = collides.GetComponent<PlayerController>();
        if (user != null)
        {
            animator.SetBool("standOn", true);
        }
    }
    //once the player exit the trigger area before it deals dmg, reset the animation
    void OnTriggerExit2D(Collider2D collides)
    {
        //Debug.Log("bye");
        animator.SetBool("standOn", false);
    }
    //for destroy the object
    void DestroySelf()
    {

        Destroy(gameObject);

    }

    //------------------Animation event---------------------
    //deals damage to player after he stands on the trap for a few moments
    //and freeze player for a few seconds
    //unfreeze the player after seconds
    void hurtPlayer()
    {
        moves.SendMessage("FreezePlayer");
        stats.SendMessage("decreaseHP", 1);
        Invoke("freePlayer", 3f);
    }

    //for unfreezing player and destroy the trap
    void freePlayer()
    {
        Debug.Log("free");
        moves.SendMessage("UnFreezePlayer");
        DestroySelf();
    }
}
