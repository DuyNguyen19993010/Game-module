using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    //animator
    private Animator animator;
    //---------------------Stat
    [Header("Max Stat")]
    public float maxHP;
    public float maxRage;
    public float damage;
    public float maxHP_temp;
    public float maxRage_temp;
    [Header("Current stat")]
    public float currentHP;
    public float Rage;
    public PlayerController playermovement;

    // Start is called before the first frame update
    void Start()
    {
        damage = 30;
        Rage = 0;
        maxHP = 100;
        currentHP = maxHP;
        maxRage = 100;
        animator = gameObject.GetComponent<Animator>();
        playermovement = gameObject.GetComponent<PlayerController>();

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

        //Disable movement for 1/2s 
        currentHP -= damage;
        if (currentHP <= 0)
        {
            //PLay death animation
        }

        // HP -= damage;

    }
    void increaseDamage(float amount)
    {
        damage += amount;
    }
    void increaseRage(float amount)
    {
        Rage += amount;
    }
    void decreaseRage(float amount)
    {
        Rage -= amount;
    }
    void resetRage()
    {
        Rage = 0;
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
