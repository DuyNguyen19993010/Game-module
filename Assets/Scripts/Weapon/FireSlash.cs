using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlash : MonoBehaviour
{
    [Header("Shooting speed")]
    public float speed;
    [Header("Appear time")]
    public float lastingTime;
    private float disapearTime;

    //----------------------------------------used to get the prefab of fire pillar that needed to be spawned
    private GameObject firePillar;
    private Rigidbody2D rb;

    void Start()
    {
        firePillar = (GameObject)Resources.Load("Prefab/Character/Player/Fire_Pillar", typeof(GameObject));
        Debug.Log(firePillar.name);
        lastingTime = 1;
        disapearTime = Time.time + lastingTime;
        speed = 2;
        GameObject player = GameObject.Find("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (player.transform.localScale.x > 0)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
    }

    void Update()
    {
        if (Time.time > disapearTime)
        {
            Destroy(gameObject);
        }
        Flip();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyStat>().SendMessage("decreaseHP", 10);
            CameraShake.Instance.ShakeCamera(0.2f, .1f);

            GameObject spawnedPillar = Instantiate(firePillar, new Vector2(other.transform.position.x, (other.transform.position.y) - 0.02f - other.GetComponent<SpriteRenderer>().bounds.size.y / 2), Quaternion.identity) as GameObject;


        }
    }
    void Flip()
    {
        if (rb.transform.localScale.x > 0 && rb.velocity.x < 0)
        {
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        else if (rb.transform.localScale.x < 0 && rb.velocity.x > 0)
        {
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

        }

    }


}
