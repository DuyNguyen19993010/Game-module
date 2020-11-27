using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Door
{
    Level_1,
    Level_2
}

public class Unlocker : MonoBehaviour
{
    public Door doorToUnlock;
    void OnDestroy()
    {
        if (doorToUnlock == Door.Level_1)
        {
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Level_1_spirit_door");
            foreach (GameObject door in doors)
            {
                Destroy(door);
            }
        }
        if (doorToUnlock == Door.Level_2)
        {
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Level_2_spirit_door");
            foreach (GameObject door in doors)
            {
                Destroy(door);
            }
        }
    }
}
