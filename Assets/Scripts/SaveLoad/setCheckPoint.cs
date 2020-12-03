using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCheckPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerStat>().checkpointPosition = transform.position;
            other.GetComponent<PlayerStat>().RestoreStat();
            other.GetComponent<InventoryManager>().ResetConsumable();
        }
    }
}
