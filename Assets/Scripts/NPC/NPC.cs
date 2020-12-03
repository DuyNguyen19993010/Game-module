using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    //pass the dialogue information to DialogueManager
    public void TriggerDialogue(){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    //finish the chat
    public void ExitTrigger(){
        FindObjectOfType<DialogueManager>().EndDialogue();
    }
}
