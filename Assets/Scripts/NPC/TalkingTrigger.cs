using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this class use to start the chat or end the chat
public class TalkingTrigger : MonoBehaviour
{
    public string NPCName;
    //if player enter the trigger area
    //start the chat
    void OnTriggerEnter2D(Collider2D collides){
        if(collides.gameObject.name.Equals("Player")){
            
            GameObject.Find(NPCName).GetComponent<NPC>().SendMessage("TriggerDialogue");
                  
        }
    }
    //if player leaves the trigger area
    //stop the chat
    void OnTriggerExit2D(Collider2D collides){
            GameObject.Find(NPCName).GetComponent<NPC>().SendMessage("ExitTrigger");
    }    
}
