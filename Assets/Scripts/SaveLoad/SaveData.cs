using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class SaveData
{
    public PlayerData playerData { get; set; }

    public SaveData()
    {

    }

}
[Serializable]
public class PlayerData
{

    //------------------Save player position-----------------------------
    public float posX { get; set; }
    public float posY { get; set; }
    //------------------Save player stat-----------------------------
    public float maxHP { get; set; }
    public float damage { get; set; }
    //------------------Save player upgrade---------------------------
    //IncreaseDamage boolean
    public bool IncreaseDamageUp { get; set; }
    //IncreaseAttackSpeed boolean
    public bool IncreaseAttackSpeed { get; set; }
    //IncreaseFortunePouch_1 boolean
    public bool IncreaseFortunePouch_1 { get; set; }
    //IncreaseFortunePouch_2 boolean
    public bool IncreaseFortunePouch_2 { get; set; }


    //--------------Save player inventory---------------------------
    //Skill inventory: SkillsInventory's elements List


    //----------------Save Quest unlocked---------------------------
    //Quest 1 boolean variable in QuestSystem gameobject boolean
    public bool Quest1 { get; set; }
    //Quest 2 boolean variable in QuestSystem gameobject boolean
    public bool Quest2 { get; set; }

    public PlayerData(float _posX, float _posY, float _maxHP, float _damage, bool _IncreaseDamageUp, bool _IncreaseAttackSpeed, bool _IncreaseFortunePouch_1, bool _IncreaseFortunePouch_2, bool _Quest1, bool _Quest2)
    {
        this.posX = _posX;
        this.posY = _posY;
        this.maxHP = _maxHP;
        this.IncreaseDamageUp = _IncreaseDamageUp;
        this.IncreaseAttackSpeed = _IncreaseAttackSpeed;
        this.IncreaseFortunePouch_1 = _IncreaseFortunePouch_1;
        this.IncreaseFortunePouch_2 = _IncreaseFortunePouch_2;
        this.Quest1 = _Quest1;
        this.Quest2 = _Quest2;

    }
}
