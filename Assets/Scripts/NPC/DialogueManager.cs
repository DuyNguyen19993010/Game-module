using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;    //create a queue to store sentences
    public Text nameOfNpc;  //UI name text
    public Text message;    //UI message text
    public Animator animator;   //animator
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();    //create emty queue
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.N)){    //if input key is N, goes to next sentence
            NextSentence();
        }
    }

    // the start of the talk
    public void StartDialogue(Dialogue dialogue){
        animator.SetBool("isShowing",true); //show the chat box
        nameOfNpc.text = dialogue.name; //UI name text is equal to the dialogue.name which is the npc's name
        sentences.Clear();  // clear all the sentences everytime you start a new talk
        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);    //store the sentences into the queue
        }
        
        NextSentence();
    }
    //prints the next sentence
    public void NextSentence(){
        if(sentences.Count == 0){   //if no more sentence left in the queue, end chat
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();  //dequeue the first sentence
        StopAllCoroutines();    //stop printing if the player goes to the next sentence before the current finished print
        StartCoroutine(TypeSentence(sentence)); //print the sentence by each letter
    }

    //print the sentence letter by letter
    IEnumerator TypeSentence(string sentence){
        message.text = "";  //initially the UI text is empty
        foreach (char letter in sentence.ToCharArray())     //add each letter in the sentence to the UI text per frame.
        {
            message.text += letter;
            yield return null;
        }
    }
    // finish the chat
    public void EndDialogue(){
        animator.SetBool("isShowing",false);
    }

}
