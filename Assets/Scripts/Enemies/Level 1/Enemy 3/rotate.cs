using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    // variable to set the target player later in the code
    private Transform targetPlayer;

    void Start()
    {
        //setting the targetplayer to our player
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }
    void Update()
    {
        try
        {
            if (transform.position.x < targetPlayer.position.x)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);

            }
            else if (transform.position.x > targetPlayer.position.x)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);

            }
        }
        catch { }
        //rotate the fireball direction towards the playe
    }
}
