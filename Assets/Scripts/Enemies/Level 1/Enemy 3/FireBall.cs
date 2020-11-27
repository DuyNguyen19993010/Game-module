using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{


    public float speed = 1f;
    public int damage;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    //adding the animation when tthe fireball touches something
    [Header("Appear time")]
    public float lastingTime;
    private float disapearTime;


    void Start()
    {
        rb.velocity = -transform.right * speed;
        lastingTime = 1;
        damage = 2;
        disapearTime = Time.time + lastingTime;
    }
    void Update()
    {
        if (Time.time > disapearTime)
        {
            Destroy(gameObject);
        }

    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if ((hitInfo.gameObject.tag == "Player") || (hitInfo.gameObject.tag == "Ground"))
        {
            Debug.Log(hitInfo);
            if (hitInfo.GetComponent<PlayerStat>() != null)
            {
                hitInfo.GetComponent<PlayerStat>().SendMessage("decreaseHP", damage);
                //if it hits the player, decrease hp
            }
            Destroy(gameObject);
            Debug.Log("Fireball destroyed");
            var clone = Instantiate(impactEffect, transform.position, transform.rotation);
            //spawn the impactEffect animation
            Destroy(clone, 0.45f);
        }
    }

}
