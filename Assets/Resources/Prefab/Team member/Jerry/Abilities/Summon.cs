using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public GameObject ally;
    bool allyInGame = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && allyInGame == false){
            spawnAlly();
            Invoke("setState", 5);
        }
    }

    void spawnAlly(){
            GameObject clone = Instantiate(ally, transform.position, transform.rotation);
            clone.gameObject.tag="Ally";
            allyInGame = true;
        destroyAlly();
    }

    void destroyAlly(){
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("Ally")){
                Destroy(o,5);
                
            
        }
        
    }
    private void setState(){
     allyInGame = false;
 }
}
