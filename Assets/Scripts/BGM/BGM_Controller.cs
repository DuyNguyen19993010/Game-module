using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Controller : MonoBehaviour
{
    public string roomNumber;   //the room number of the bgm is playing 
    public string levelNumber;  //the level number of the room
    private AudioSource m_MyAudioSource;    //get the background music source
    // when player enter the rooms
    //find the correct levelnumber and then the correct roomnumber
    //then plays its background music
    //keep it looping
    void OnTriggerEnter2D(Collider2D collides){
        if(collides.gameObject.name.Equals("Player")){
            m_MyAudioSource = GameObject.Find(levelNumber).transform.Find(roomNumber).GetComponent<AudioSource>();
            m_MyAudioSource.Play();
            m_MyAudioSource.loop = true;
        }
    }
    //once player leaves the room
    //stop playing the music
    void OnTriggerExit2D(Collider2D collides){
        if(collides.gameObject.name.Equals("Player")){
            m_MyAudioSource.Stop();
        }
    }
}
