using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float speed = 1f;   //speed of the projectile
    public Rigidbody2D rb;
    private PlayerStat stats;
    private BomberAttack bomberattack;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        stats = GameObject.Find("Player").GetComponent<PlayerStat>();   //get the playerstat script
        bomberattack = GameObject.Find("Bomber").GetComponent<BomberAttack>();  //get the BomberAttack script
        rb.velocity = transform.right * speed;  // throw the bomb out       
    }

    // animation event that used to damage the player if player is in the damge range when the bomb explodes
    void Damage()
    {
        rb.velocity = transform.right * 0; // stop the projectile move when it explodes
        if (Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) < 0.2)
        {
            stats.SendMessage("decreaseHP", stats.damage);
        }
    }

    // animation event that used to set the next attack avaiable after the last attack is finished and destroy the bomb prefab
    void ResetAttack()
    {
        Destroy(gameObject);

        try
        {

            bomberattack.SendMessage("ResetAttack");
        }
        catch { }
    }
}
