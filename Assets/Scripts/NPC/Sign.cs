using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    
    public bool playerInRange;
    public string[] dialog;
    
    public int currentDialogue;
    private bool DialogueBoxOpen;

    private bool DialogueEnded;
    
    void Start()
    {
        currentDialogue = 0;
        DialogueBoxOpen = false;
        DialogueEnded = false;
        
    }
    void Update()
    {
        if(playerInRange){
            
            if(Input.GetKeyDown(KeyCode.L))
            {Debug.Log(DialogueEnded);

                if(DialogueBoxOpen == true)
                {
                    if(DialogueEnded == true)
                    {
                        dialogBox.SetActive(false);
                        currentDialogue = 0;
                        DialogueBoxOpen = false;
                    }
                    else{
                    nextDialogueLine();
                    }
                }
    
                else{
                    Debug.Log("else");
                    DialogueBoxOpen = true;
                    dialogBox.SetActive(true);
                    DialogueEnded =false;
                }
            }
        }
        displayText();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("player"))
        {
            Debug.Log("Player in range");
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.CompareTag("player"))
        {
            Debug.Log("Player left range");
            playerInRange = false;
            dialogBox.SetActive(false);
            currentDialogue = 0;
            DialogueBoxOpen = false;
            DialogueEnded =false;
        }
    }


    private void displayText(){

            dialogText.text = dialog[currentDialogue];
       
            
    }

    private void nextDialogueLine(){

        currentDialogue += 1;

        if(currentDialogue >= dialog.Length-1)
        {
            DialogueEnded = true;
            currentDialogue = dialog.Length - 1;
        }

    }
    

}

