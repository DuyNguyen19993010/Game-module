using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBossSpawner : MonoBehaviour
{
    public GameObject Boss;
    public Transform spawnPosition;
    public Quest questManager;
    public int roomNo;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (roomNo == 1)
        {
            if (!questManager.Quest_1_finish)
            {
                Instantiate(Boss, spawnPosition);
            }
        }
        if (roomNo == 2)
        {
            if (!questManager.Quest_2_finish)
            {
                Instantiate(Boss, spawnPosition);
            }
        }


    }

}
