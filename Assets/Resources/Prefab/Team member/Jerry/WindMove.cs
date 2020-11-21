using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMove : MonoBehaviour
{
    private float speed = 2f;
    public Rigidbody2D rb;

    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * -speed;
    }


    void OnTriggerEnter2D(Collider2D hitted)
    {
        PlayerController playermovement = hitted.GetComponent<PlayerController>();
        PlayerStat playerstat = hitted.GetComponent<PlayerStat>();
        if (playermovement != null)
        {
            playerstat.SendMessage("decreaseHP", 10);
            Destroy(gameObject);
        }

    }

}
