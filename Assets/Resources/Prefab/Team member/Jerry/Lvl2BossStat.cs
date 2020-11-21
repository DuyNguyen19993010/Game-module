using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl2BossStat : MonoBehaviour
{
    private float maxHP;
    private float currentHP;

    // Start is called before the first frame update
    void Start()
    {
        maxHP = 500;
        currentHP = maxHP;
    }

    void decreaseHP(float damage)
    {
        // Boss hurt animation


        currentHP -= damage;
        if (currentHP <= 0)
        {
            //Boss death animation
        }

    

    }
}
