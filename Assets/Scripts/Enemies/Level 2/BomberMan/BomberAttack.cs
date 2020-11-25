using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAttack : MonoBehaviour
{

    public Transform firepoint; //locate the firepoint which is the throwpoint game object
    public GameObject BombPrefab;   //locate the bomb prefab
    private Animator animator;  //animator of the bomber
    public bool nextAttack; //decide the next attack is ready or not
    private bool faceRight; //check the face direction
    
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        nextAttack = true;
        faceRight = true;   //initially face right
    }

    // Update is called once per frame
    void Update()
    {
        //------detect the distance between player and bomber and decide to change face direction or not------
        if(transform.position.x - GameObject.Find("Player").transform.position.x > 0 && faceRight){
            flip();
        }
        else if(transform.position.x - GameObject.Find("Player").transform.position.x < 0 && !faceRight){
            flip();
        }
        //-----------------------------------------------------------------------------------------------
        //-----detect if the player is inside the attack range from bomber-------
        if(Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) < 1){
            if(nextAttack){
            animator.SetBool("isThrowing",true);
            nextAttack = false;
            }
        }
        
    }

    //animation event as instantiate the bomb
    void ThrowOut(){
        Instantiate(BombPrefab,firepoint.position,firepoint.rotation);
        
        animator.SetBool("isThrowing", false);
    }
    //make the next attack available
    void ResetAttack(){
        nextAttack = true;
    }
    // flip the bomber
    void flip(){
        faceRight = !faceRight;
        transform.Rotate(0f,180f,0f);
    }

    
}
