using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoAttack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hitted){
        PlayerController playermovement = hitted.GetComponent<PlayerController>();
        PlayerStat playerstat = hitted.GetComponent<PlayerStat>();
        if(playermovement != null){
            playermovement.SendMessage("setMoving", false);
            playerstat.SendMessage("decreaseHP", 10);
        }
    }

}
