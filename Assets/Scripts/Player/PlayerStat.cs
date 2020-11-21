using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    //---------------------------Player ------------------------------
    public Player player;
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
    public bool canBeHurt;

    // Start is called before the first frame update
    void Start()
    {
        player = new Player(10, 10, 5);
        canBeHurt = true;
        maxHP = player.maxHP;
        maxRage = player.maxRage;
        damage = player.damage;
        currentHP = maxHP;
        Rage = 0;
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
        Debug.Log("Increase by" + amount);
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    void decreaseHP(float damage)
    {


        if (canBeHurt)
        {
            Debug.Log("Hurt");
            // Play hurt animation
            currentHP -= damage;
            if (currentHP <= 0)
            {
                currentHP = 0;
                //PLay death animation
            }
        }


        // HP -= damage;

    }
    void increaseDamage(float amount)
    {
        damage += amount;
    }
    void decreaseDamage(float amount)
    {
        damage -= amount;
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
