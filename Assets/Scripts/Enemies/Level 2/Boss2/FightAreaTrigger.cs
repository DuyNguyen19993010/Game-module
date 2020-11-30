using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightAreaTrigger : MonoBehaviour
{   //-------------------------if player enters the battle area start the fight--------------------------
    void OnTriggerEnter2D(Collider2D collides){
        if(collides.gameObject.name.Equals("Player")){
            GameObject.Find("Moon_Boss").GetComponent<MoonBoss_Attack>().SendMessage("startFight");
        }
    }
}
