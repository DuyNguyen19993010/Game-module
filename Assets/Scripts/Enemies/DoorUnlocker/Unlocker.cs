using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Door
{
    Level_1,
    Level_15,
    Level_2
}


public class Unlocker : MonoBehaviour
{
    public Quest questList;

    public Door doorToUnlock;
    void Update()
    {
        if (doorToUnlock == Door.Level_1)
        {
            if (questList.Quest_1_finish)
            {
                Destroy(gameObject);
            }
        }

        if (doorToUnlock == Door.Level_15)
        {
            if (questList.Quest_15_finish)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnDestroy()
    {
        if (doorToUnlock == Door.Level_1)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Attacks>().UnlockSkill(1);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Attacks>().UnlockSkill(0);
            questList.Quest_1_finish = true;
        }
        if (doorToUnlock == Door.Level_15)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Attacks>().UnlockSkill(3);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Attacks>().UnlockSkill(2);
            questList.Quest_15_finish = true;
        }
    }
}
