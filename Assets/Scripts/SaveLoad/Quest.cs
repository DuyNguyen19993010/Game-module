using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public bool Quest_1_finish;
    public bool Quest_15_finish;
    public bool Quest_2_finish;
    void Start()
    {
        Quest_1_finish = false;
        Quest_15_finish = false;
        Quest_2_finish = false;

    }
    void Update()
    {
        if (Quest_1_finish)
        {
            //If Level 1 is clear destroy all spirit door 1 
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Level_1_spirit_door");
            foreach (GameObject door in doors)
            {
                Destroy(door);
            }
        }
        if (Quest_15_finish)
        {

        }
        if (Quest_2_finish)
        {
            //If Level 1 is clear destroy all spirit door 2
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Level_2_spirit_door");
            foreach (GameObject door in doors)
            {
                Destroy(door);
            }

        }
    }

}
