using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    //private PlayerStat stats;
    // Start is called before the first frame update
    void Start()
    {
        //stats = GameObject.Find("Player").GetComponent<PlayerStat>();
    }

    //while player touches the lava, kill the player
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == ("Player") || other.tag == ("Enemy"))
        {
            //stats.SendMessage("decreaseHP", 10);
            Destroy(other.gameObject);
        }

    }
}
