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

    // Start is called before the first frame update
    void Start()
    {
        Rage = 0;
        maxHP = 100;
        currentHP = maxHP;
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

    IEnumerator decreaseHP(float damage)
    {
        // Play hurt animation
        animator.SetTrigger("hurt");



        //Disable movement for 1/2s 
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        gameObject.GetComponent<PlayerController>().SendMessage("setMoving", false);
        animator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(1 / 2);
        currentHP -= damage;


        //Enable movement
        gameObject.GetComponent<PlayerController>().SendMessage("setMoving", true);
        // HP -= damage;

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
