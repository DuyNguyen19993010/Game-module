using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
public class SaveManager : MonoBehaviour
{
    //Player gameobject
    public GameObject player;
    //Player stat
    public PlayerStat playerStat;
    //Player attack
    public Attacks playerAttack;
    //Player Upgrades
    public Upgrades playerUpgrade;
    //Quest Manager
    public Quest quests;
    //Skill UI
    public Skill_UI skill_UI;
    //------------------------NEED SAVING---------------------------------
    //------------------Save player position-----------------------------
    public float posX;//
    public float posY;//
    //------------------Save player upgrade---------------------------
    //IncreaseDamage boolean
    public bool IncreaseDamageUp;
    //IncreaseAttackSpeed boolean
    public bool IncreaseAttackSpeed;
    //IncreaseFortunePouch_1 boolean
    public bool IncreaseFortunePouch_1;
    //IncreaseFortunePouch_2 boolean
    public bool IncreaseFortunePouch_2;


    //--------------Save player inventory---------------------------
    //Skill inventory: SkillsInventory's elements List
    public bool fire_skill;
    public bool moon_skill;
    public bool shiroinu;
    public bool kuroiniu;


    //----------------Save Quest unlocked---------------------------
    //Quest 1 boolean variable in QuestSystem gameobject boolean
    public bool Quest1 { get; set; }
    //Village quest boolean variable in QuestSystem gameobject boolean
    public bool Quest15 { get; set; }
    //Quest 2 boolean variable in QuestSystem gameobject boolean
    public bool Quest2 { get; set; }

    //-------------------------------Get tutorial manager-----------------------
    public TutorialManager tutorialManager;
    public bool tutorial_1_done;
    public bool tutorial_2_done;
    public bool tutorial_3_done;
    void Start()
    {
        FindSaveInfo();
        if (PlayerPrefs.GetInt("value") == 1)
        {
            Load();
        }
        if (PlayerPrefs.GetInt("value") == 2)
        {
            Respawn();
        }

    }
    private void FindSaveInfo()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStat = player.GetComponent<PlayerStat>();
        playerAttack = player.GetComponent<Attacks>();
        playerUpgrade = player.GetComponent<Upgrades>();
        quests = GameObject.Find("QuestSystem").GetComponent<Quest>();
        tutorialManager = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
    }
    //-----------------------------------Normal Save-----------------------------------------------

    public void Save()
    {
        FindSaveInfo();
        //---------Position------------
        posX = playerStat.checkpointPosition.x;
        posY = playerStat.checkpointPosition.y;
        //Save Position
        PlayerPrefs.SetFloat("posX", posX);
        PlayerPrefs.SetFloat("posY", posY);
        //Debug
        Debug.Log("X position: " + PlayerPrefs.GetFloat("posX"));
        Debug.Log("Y position: " + PlayerPrefs.GetFloat("posY"));


        //---------Upgrade------------
        IncreaseDamageUp = playerUpgrade.IncreaseDamage;
        IncreaseAttackSpeed = playerUpgrade.IncreaseAttackSpeed;
        IncreaseFortunePouch_1 = playerUpgrade.IncreaseFortunePouch_1;
        IncreaseFortunePouch_2 = playerUpgrade.IncreaseFortunePouch_2;
        //Save Upgrades
        PlayerPrefs.SetInt("IncreaseDamageUp", boolToInt(IncreaseDamageUp));
        PlayerPrefs.SetInt("IncreaseAttackSpeed", boolToInt(IncreaseAttackSpeed));
        PlayerPrefs.SetInt("IncreaseFortunePouch_1", boolToInt(IncreaseFortunePouch_1));
        PlayerPrefs.SetInt("IncreaseFortunePouch_2", boolToInt(IncreaseFortunePouch_2));
        //Debug
        Debug.Log("IncreaseDamageUp:" + PlayerPrefs.GetInt("IncreaseDamageUp"));
        Debug.Log("IncreaseAttackSpeed:" + PlayerPrefs.GetInt("IncreaseAttackSpeed"));
        Debug.Log("IncreaseFortunePouch_1:" + PlayerPrefs.GetInt("IncreaseFortunePouch_1"));
        Debug.Log("IncreaseFortunePouch_2:" + PlayerPrefs.GetInt("IncreaseFortunePouch_2"));




        //---------Skills------------
        fire_skill = playerStat.player.skills.GetSkillList()[0].enabled;
        moon_skill = playerStat.player.skills.GetSkillList()[1].enabled;
        shiroinu = playerStat.player.skills.GetSkillList()[2].enabled;
        kuroiniu = playerStat.player.skills.GetSkillList()[3].enabled;
        //Save Skills
        PlayerPrefs.SetInt("fire_skill", boolToInt(fire_skill));
        PlayerPrefs.SetInt("moon_skill", boolToInt(moon_skill));
        PlayerPrefs.SetInt("shiroinu", boolToInt(shiroinu));
        PlayerPrefs.SetInt("kuroiniu", boolToInt(kuroiniu));
        //Debug
        Debug.Log("fire_skill:" + PlayerPrefs.GetInt("fire_skill"));
        Debug.Log("moon_skill:" + PlayerPrefs.GetInt("moon_skill"));
        Debug.Log("shiroinu:" + PlayerPrefs.GetInt("shiroinu"));
        Debug.Log("kuroiniu:" + PlayerPrefs.GetInt("kuroiniu"));



        //--------Quests--------------
        Quest1 = quests.Quest_1_finish;
        Quest15 = quests.Quest_15_finish;
        Quest2 = quests.Quest_2_finish;
        PlayerPrefs.SetInt("Quest1", boolToInt(Quest1));
        PlayerPrefs.SetInt("Quest15", boolToInt(Quest15));
        PlayerPrefs.SetInt("Quest2", boolToInt(Quest2));
        //-------------Tutorial------------------
        tutorial_1_done = tutorialManager.tutorial_1_done;
        tutorial_2_done = tutorialManager.tutorial_2_done;
        tutorial_3_done = tutorialManager.tutorial_3_done;
        //Save tutorial
        PlayerPrefs.SetInt("tutorial_1_done", boolToInt(tutorial_1_done));
        PlayerPrefs.SetInt("tutorial_2_done", boolToInt(tutorial_2_done));
        PlayerPrefs.SetInt("tutorial_3_done", boolToInt(tutorial_3_done));
    }


    public void Load()
    {
        FindSaveInfo();
        //---------Position------------
        player.transform.position = new Vector2(PlayerPrefs.GetFloat("posX"), PlayerPrefs.GetFloat("posY"));
        //---------Upgrade------------
        playerUpgrade.IncreaseDamage = intToBool(PlayerPrefs.GetInt("IncreaseDamageUp"));
        playerUpgrade.IncreaseAttackSpeed = intToBool(PlayerPrefs.GetInt("IncreaseAttackSpeed"));
        playerUpgrade.IncreaseFortunePouch_1 = intToBool(PlayerPrefs.GetInt("IncreaseFortunePouch_1"));
        playerUpgrade.IncreaseFortunePouch_2 = intToBool(PlayerPrefs.GetInt("IncreaseFortunePouch_2"));
        playerUpgrade.UpgradePlayer();
        //---------Skills------------
        playerStat.player.skills.GetSkillList()[0].enabled = intToBool(PlayerPrefs.GetInt("fire_skill"));
        playerStat.player.skills.GetSkillList()[1].enabled = intToBool(PlayerPrefs.GetInt("moon_skill"));
        playerStat.player.skills.GetSkillList()[2].enabled = intToBool(PlayerPrefs.GetInt("shiroinu"));
        playerStat.player.skills.GetSkillList()[3].enabled = intToBool(PlayerPrefs.GetInt("kuroiniu"));
        skill_UI.refresh = true;
        //--------Quests--------------
        quests.Quest_1_finish = intToBool(PlayerPrefs.GetInt("Quest1"));
        quests.Quest_15_finish = intToBool(PlayerPrefs.GetInt("Quest15"));
        quests.Quest_2_finish = intToBool(PlayerPrefs.GetInt("Quest2"));
        //-------Tutorial-------------
        tutorialManager.tutorial_1_done = intToBool(PlayerPrefs.GetInt("re_tutorial_1_done"));
        tutorialManager.tutorial_2_done = intToBool(PlayerPrefs.GetInt("re_tutorial_2_done"));
        tutorialManager.tutorial_3_done = intToBool(PlayerPrefs.GetInt("re_tutorial_3_done"));
    }




    //-----------------------------------Respawn save-----------------------------------------------


    public void RespawnSave()
    {
        FindSaveInfo();
        //---------Position------------
        posX = playerStat.checkpointPosition.x;
        posY = playerStat.checkpointPosition.y;
        //Save Position
        PlayerPrefs.SetFloat("re_posX", posX);
        PlayerPrefs.SetFloat("re_posY", posY);
        //Debug
        Debug.Log("X position: " + PlayerPrefs.GetFloat("re_re_posX"));
        Debug.Log("Y position: " + PlayerPrefs.GetFloat("re_posY"));
        Debug.Log("Y position: " + PlayerPrefs.GetFloat("re_posY"));


        //---------Upgrade------------
        IncreaseDamageUp = playerUpgrade.IncreaseDamage;
        IncreaseAttackSpeed = playerUpgrade.IncreaseAttackSpeed;
        IncreaseFortunePouch_1 = playerUpgrade.IncreaseFortunePouch_1;
        IncreaseFortunePouch_2 = playerUpgrade.IncreaseFortunePouch_2;
        //Save Upgrades
        PlayerPrefs.SetInt("re_IncreaseDamageUp", boolToInt(IncreaseDamageUp));
        PlayerPrefs.SetInt("re_IncreaseAttackSpeed", boolToInt(IncreaseAttackSpeed));
        PlayerPrefs.SetInt("re_IncreaseFortunePouch_1", boolToInt(IncreaseFortunePouch_1));
        PlayerPrefs.SetInt("re_IncreaseFortunePouch_2", boolToInt(IncreaseFortunePouch_2));
        //Debug
        Debug.Log("IncreaseDamageUp:" + PlayerPrefs.GetInt("re_IncreaseDamageUp"));
        Debug.Log("IncreaseAttackSpeed:" + PlayerPrefs.GetInt("re_IncreaseAttackSpeed"));
        Debug.Log("IncreaseFortunePouch_1:" + PlayerPrefs.GetInt("re_IncreaseFortunePouch_1"));
        Debug.Log("IncreaseFortunePouch_2:" + PlayerPrefs.GetInt("re_IncreaseFortunePouch_2"));




        //---------Skills------------
        fire_skill = playerStat.player.skills.GetSkillList()[0].enabled;
        moon_skill = playerStat.player.skills.GetSkillList()[1].enabled;
        shiroinu = playerStat.player.skills.GetSkillList()[2].enabled;
        kuroiniu = playerStat.player.skills.GetSkillList()[3].enabled;
        //Save Skills
        PlayerPrefs.SetInt("re_fire_skill", boolToInt(fire_skill));
        PlayerPrefs.SetInt("re_moon_skill", boolToInt(moon_skill));
        PlayerPrefs.SetInt("re_shiroinu", boolToInt(shiroinu));
        PlayerPrefs.SetInt("re_kuroiniu", boolToInt(kuroiniu));
        //Debug
        Debug.Log("fire_skill:" + PlayerPrefs.GetInt("re_fire_skill"));
        Debug.Log("moon_skill:" + PlayerPrefs.GetInt("re_moon_skill"));
        Debug.Log("shiroinu:" + PlayerPrefs.GetInt("re_shiroinu"));
        Debug.Log("kuroiniu:" + PlayerPrefs.GetInt("re_kuroiniu"));



        //--------Quests--------------
        Quest1 = quests.Quest_1_finish;
        Quest15 = quests.Quest_15_finish;
        Quest2 = quests.Quest_2_finish;
        PlayerPrefs.SetInt("re_Quest1", boolToInt(Quest1));
        PlayerPrefs.SetInt("re_Quest15", boolToInt(Quest15));
        PlayerPrefs.SetInt("re_Quest2", boolToInt(Quest2));



        //-------------Tutorial------------------
        tutorial_1_done = tutorialManager.tutorial_1_done;
        tutorial_2_done = tutorialManager.tutorial_2_done;
        tutorial_3_done = tutorialManager.tutorial_3_done;
        //Save tutorial
        PlayerPrefs.SetInt("re_tutorial_1_done", boolToInt(tutorial_1_done));
        PlayerPrefs.SetInt("re_tutorial_2_done", boolToInt(tutorial_2_done));
        PlayerPrefs.SetInt("re_tutorial_3_done", boolToInt(tutorial_3_done));

    }
    public void Respawn()
    {
        FindSaveInfo();
        //---------Position------------
        player.transform.position = new Vector2(PlayerPrefs.GetFloat("re_posX"), PlayerPrefs.GetFloat("re_posY"));
        //---------Upgrade------------
        playerUpgrade.IncreaseDamage = intToBool(PlayerPrefs.GetInt("re_IncreaseDamageUp"));
        playerUpgrade.IncreaseAttackSpeed = intToBool(PlayerPrefs.GetInt("re_IncreaseAttackSpeed"));
        playerUpgrade.IncreaseFortunePouch_1 = intToBool(PlayerPrefs.GetInt("re_IncreaseFortunePouch_1"));
        playerUpgrade.IncreaseFortunePouch_2 = intToBool(PlayerPrefs.GetInt("re_IncreaseFortunePouch_2"));
        playerUpgrade.UpgradePlayer();
        //---------Skills------------
        playerStat.player.skills.GetSkillList()[0].enabled = intToBool(PlayerPrefs.GetInt("re_fire_skill"));
        playerStat.player.skills.GetSkillList()[1].enabled = intToBool(PlayerPrefs.GetInt("re_moon_skill"));
        playerStat.player.skills.GetSkillList()[2].enabled = intToBool(PlayerPrefs.GetInt("re_shiroinu"));
        playerStat.player.skills.GetSkillList()[3].enabled = intToBool(PlayerPrefs.GetInt("re_kuroiniu"));
        skill_UI.refresh = true;
        //--------Quests--------------
        quests.Quest_1_finish = intToBool(PlayerPrefs.GetInt("re_Quest1"));
        quests.Quest_15_finish = intToBool(PlayerPrefs.GetInt("re_Quest15"));
        quests.Quest_2_finish = intToBool(PlayerPrefs.GetInt("re_Quest2"));
        //-------Tutorial-------------
        tutorialManager.tutorial_1_done = intToBool(PlayerPrefs.GetInt("re_tutorial_1_done"));
        tutorialManager.tutorial_2_done = intToBool(PlayerPrefs.GetInt("re_tutorial_2_done"));
        tutorialManager.tutorial_3_done = intToBool(PlayerPrefs.GetInt("re_tutorial_3_done"));


    }


    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }
    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
}
