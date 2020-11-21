using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    //--------------------Player's stat---------------
    public PlayerStat playerStat;//--USed to get the player's current number of health
    private GameObject healthIcon;
    void Awake()
    {
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
        healthIcon = (GameObject)Resources.Load("Prefab/UI/Health", typeof(GameObject));
    }
    void Update()
    {
        if (transform.childCount != playerStat.currentHP)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<HealtIconBehaviour>().PlayDestroyAnimation();

            }

            Debug.Log("rendered");
            for (int i = 0; i < playerStat.currentHP; i++)
            {
                Instantiate(healthIcon, transform);
            }
        }

    }
}
