using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Flight : MonoBehaviour
{



    private Transform targetPlayer;
    // variable to set the target player later in the code 
    public float speed;
    //speed of the enemy
    public float distance;
    //this represents the distance between the player and the boss
    public float timeAttack = 0.55f;
    private float time = 0.0f;
    //when the enemy detects the player
    public float DetectRange = 2;


    // Start is called before the first frame update
    void Start()
    {
        //setting the targetplayer to our player
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        //if statement to asure we keep a distance between player and enemy2

        try
        {
            if (Vector2.Distance(transform.position, targetPlayer.position) < DetectRange)
            {
                followPlayer();
            }

            time += Time.deltaTime;

            if (Vector2.Distance(transform.position, targetPlayer.position) <= distance)
            {
                if (time >= timeAttack)
                {
                    time = 0.0f;
                    DamagePlayer(gameObject.GetComponent<EnemyStat>().damage);

                }
            }
        }
        catch
        {

        }

    }

    //here we define the function that is used to follow the player
    public void followPlayer()
    {
        if (Vector2.Distance(transform.position, targetPlayer.position) >= distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, speed * Time.deltaTime);
        }
        if (transform.position.x < targetPlayer.position.x)
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
        else
        {
            transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f);
        }
    }

    //use to call the decreaseHP function from the player script and apply some damage
    public void DamagePlayer(float damage)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>().SendMessage("decreaseHP", damage);
    }

}
