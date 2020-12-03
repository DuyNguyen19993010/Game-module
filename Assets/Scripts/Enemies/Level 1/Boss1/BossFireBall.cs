using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall : MonoBehaviour
{


    public float speed = 1f;
    public int damage = 2;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public float lastingTime;
    private float disapearTime;


    void Start()
    {
        rb.velocity = -transform.up * speed;
        lastingTime = 1;
        damage = 2;
        disapearTime = Time.time + lastingTime;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if ((hitInfo.gameObject.tag == "Player") || (hitInfo.gameObject.tag == "Ground"))
        {


            if (hitInfo.GetComponent<PlayerStat>() != null)
            {
                hitInfo.GetComponent<PlayerStat>().SendMessage("decreaseHP", damage);
            }
            Destroy(gameObject);
            var clone = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(clone, 0.45f);
        }
    }

}
