using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    //animator
    private Animator animator;
    //---------------------Stat
    public PlayerController playermovement;
    public float currentHP;
    public float maxHP;
    public float maxHP_temp;
    public float Rage;
    public float maxRage;
    public float maxRage_temp;
    public Health health;
    public Rage rage;

    // Start is called before the first frame update
    void Start()
    {
        Rage = 0;
        maxHP = 100;
        currentHP = maxHP;
        maxRage = 100;
        animator = gameObject.GetComponent<Animator>();
        playermovement = gameObject.GetComponent<PlayerController>();
        health.SetMaxHealth(maxHP);
        rage.SetMaxRage(maxRage);

    }
    void Update()
    {
    }
    // Update is called once per frame


    // Modification for current real time stat
    void increaseHP(float amount)
    {
        currentHP += amount;
    }

    void decreaseHP(float damage)
    {
        // Play hurt animation
        animator.SetTrigger("hurt");
        Debug.Log("Player's health:" + currentHP);



        //Disable movement for 1/2s 
        animator.SetFloat("Speed", 0);
        currentHP -= damage;
        health.SetHealth(currentHP);
        if (currentHP <= 0)
        {
            GameObject temp_player = gameObject;
            gameObject.SetActive(false);
        }
        //Enable movement
        gameObject.GetComponent<PlayerController>().SendMessage("setMoving", true);
        // HP -= damage;

    }
    void increaseRage(float amount)
    {
        Rage += amount;
        rage.SetRage(Rage);
    }
    void decreaseRage(float amount)
    {
        Rage -= amount;
    }
    void resetRage()
    {
        Rage = 0;
        rage.SetRage(Rage);
    }





    // Modification for current permanent stat

    void increaseMaxHP(float amount)
    {
        maxHP_temp = maxHP;
        maxHP += amount;
    }

    void decreaseMaxHP(float amount)
    {
        maxHP_temp = maxHP;
        maxHP += amount;
    }

    void increaseMaxRage(float amount)
    {
        maxRage_temp += amount;
        maxRage += amount;
    }

    void decreaseMaxRage(float amount)
    {
        maxRage_temp += amount;
        maxHP += amount;

    }


}
