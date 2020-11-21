using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    //---------------------Stat
    [Header("Max Stat")]
    public float maxHP;
    public float maxRage;
    public float damage;
    public Skills skills;
    public Player(float _maxHP, float _maxRage, float _damage)
    {
        maxHP = _maxHP;
        maxRage = _maxRage;
        damage = _damage;
        skills = new Skills();
    }
}
