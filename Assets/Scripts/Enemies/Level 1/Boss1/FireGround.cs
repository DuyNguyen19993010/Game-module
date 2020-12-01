using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGround : MonoBehaviour
{
    
    [HideInInspector]
    public float lastingTime;
    private float disapearTime;
    public int damage;
    private Transform targetPlayer;
    void Start()
    {
        lastingTime = 4;
        damage = 2;
        disapearTime = Time.time + lastingTime;
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

   
    void Update()
    {
        if(Time.time > disapearTime)
        {
            Destroy(gameObject);
        }
        //make fire disapear after 4 seconds
    }
   

    public void DamagePlayer(){
        if(Vector2.Distance(transform.position, targetPlayer.position) < 0.3f)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>().SendMessage("decreaseHP", 1);  
        }


    }

}
