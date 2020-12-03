using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == ("Player") || other.tag == ("Enemy"))
        {
            if (other.tag == ("Player"))
            {
                other.GetComponent<PlayerStat>().SendMessage("decreaseHP", 100);
            }
            else if (other.tag == ("Enemy"))
            {
                other.GetComponent<EnemyStat>().SendMessage("decreaseHP", 100);
            }

        }

    }
}
