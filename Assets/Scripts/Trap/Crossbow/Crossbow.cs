using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public Animator animator;   //get animator of the bow
    private Rigidbody2D rb; 
    private PlayerStat stats;   //get Playerstat script
    public Transform firepoint; //locate the firepoint
    public GameObject bowPrefab;    //get the Arrow prefab
    private RaycastHit2D playerCheck; //use raycast to detect player
    private float detectRadius; // detectRadius of the raycast
    public LayerMask playerlayer;
    private bool canAttack; //make gap between attacks
    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.Find("Player").GetComponent<PlayerStat>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        detectRadius = 1f;
        canAttack = true;   //initially set to true
    }

    // Update is called once per frame
    void Update()
    {
        playerCheck = Physics2D.Raycast(firepoint.position, -Vector2.right,detectRadius,playerlayer);   //get the raycast objects that starts at firepoint's position, left direction(as the bow is placed facing left), detectRadius, objects sets to player layer
        //if hit something
        if(playerCheck.collider != null){
            //check canAttack or not
            if(canAttack){
                animator.SetTrigger("Attack");  //Attack animation start
                canAttack = false;  //set canAttack to false
                Invoke("ResetAttack",2f);   //Reset canAttack to true after 2s.
            }
        }
    }

    //animation event-------------------
    //used to instantiate the arrow
    void bowShoot(){
        Instantiate(bowPrefab,firepoint.position,firepoint.rotation);
    }
    //Set canAttack to true------------------
    void ResetAttack(){
        canAttack = true;
    }
}
