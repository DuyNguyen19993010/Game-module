using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xpDrop : MonoBehaviour
{
    public float value;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            other.GetComponent<PlayerStat>().SendMessage("increaseXP", value);
            Debug.Log("Player Xp increased");
            // Debug.Log("XP:" + other.GetComponent<PlayerStat>().currentXP);
            // Debug.Log("Level:" + other.GetComponent<PlayerStat>().Level);
        }
    }
}
