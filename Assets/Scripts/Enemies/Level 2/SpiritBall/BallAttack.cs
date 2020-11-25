using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttack : MonoBehaviour
{
    public EnemyMovement movement;  //get the enemymovement script
    public Transform firepoint; //locate the firepoint
    public GameObject shootballPrefab;  //get the shots prefab
    private Animator animator;  //animator of the spirit ball
    public bool nextAttack; //decide next attack is avaiable or not

    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.GetComponent<EnemyMovement>();
        animator = gameObject.GetComponent<Animator>();
        nextAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if spirit ball is following the player then it can attack
        if(movement.followingPlayer){
            if(nextAttack){
                shooting();
                nextAttack=false;
                StartCoroutine(shootAgain(1.5f));   //make the attack availiable after 1.5s
            }
        }
    }

    //shoot the ball projectile at firepoint position
    void shooting(){
        Instantiate(shootballPrefab,firepoint.position,firepoint.rotation);
    }

    //set the next attack availiable after 1.5s
    private IEnumerator shootAgain(float waitTime){
        yield return new WaitForSeconds(waitTime);
        nextAttack = true;
    }
}
