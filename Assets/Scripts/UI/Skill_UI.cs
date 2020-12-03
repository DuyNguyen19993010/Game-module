using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_UI : MonoBehaviour
{
    //---------------------------Get player attack input------------------------------------------
    private Attacks playerAttack;
    private PlayerStat playerStat;
    //--------------------Decide to refresh the ui or not--------------
    public bool refresh;
    //---------------------------Calculate and render the cool down for each skill----------------
    [Header("Fire Skill")]
    public Image FireSkill;
    private float fire_skill_cooldown;
    bool fire_skill_isCoolDown;
    //---------------------------Calculate and render the cool down for each skill----------------
    [Header("Moon Skill")]
    public Image MoonSkill;
    public float moon_skill_cooldown;
    bool moon_skill_isCoolDown;


    [Header("Shiroinu")]
    public Image Shiroinu;
    private float shiroinu_cooldown;
    bool shiroinu_isCoolDown;


    [Header("Kuroinu")]

    public Image Kuroinu;
    private float kuroinu_cooldown;
    bool kuroinu_isCoolDown;


    void Start()
    {
        refresh = false;
        playerAttack = GameObject.Find("Player").gameObject.GetComponent<Attacks>();
        playerStat = GameObject.Find("Player").gameObject.GetComponent<PlayerStat>();
        //-----------------Setting fire skill attribute------------------------
        fire_skill_cooldown = playerAttack.fire_skill_cooldown;
        FireSkill.fillAmount = 0;
        fire_skill_isCoolDown = false;
        //-----------------Setting moon skill attribute------------------------
        moon_skill_cooldown = playerAttack.moon_skill_cooldown;
        MoonSkill.fillAmount = 0;
        moon_skill_isCoolDown = false;
        //-----------------Setting shiroinu skill attribute------------------------
        shiroinu_cooldown = playerAttack.shiroinu_cooldown;
        Shiroinu.fillAmount = 0;
        shiroinu_isCoolDown = false;
        //-----------------Setting kuroinu skill attribute------------------------
        kuroinu_cooldown = playerAttack.kuroinu_cooldown;
        Kuroinu.fillAmount = 0;
        kuroinu_isCoolDown = false;
    }
    void Update()
    {
        if (refresh)
        {
            Refresh();

        }
        else
        {
            if (playerStat.player.skills.GetSkillList()[0].enabled)
            {

                Fire_Skill();
            }
            else
            {
                FireSkill.fillAmount = 1;

            }


            if (playerStat.player.skills.GetSkillList()[1].enabled)
            {

                Moon_Skill();
            }
            else
            {
                MoonSkill.fillAmount = 1;
            }



            if (playerStat.player.skills.GetSkillList()[2].enabled)
            {

                Shiroinu_skill();
            }
            else
            {
                Shiroinu.fillAmount = 1;
            }




            if (playerStat.player.skills.GetSkillList()[3].enabled)
            {

                Kuroinu_skill();
            }
            else
            {
                Kuroinu.fillAmount = 1;
            }





        }
    }
    public void Refresh()
    {
        FireSkill.fillAmount = 0;
        MoonSkill.fillAmount = 0;
        Shiroinu.fillAmount = 0;
        Kuroinu.fillAmount = 0;
        refresh = false;

    }
    void Fire_Skill()
    {
        if (playerAttack.isUsingFireSkill && fire_skill_isCoolDown == false)
        {
            fire_skill_isCoolDown = true;
            FireSkill.fillAmount = 1;
        }

        if (fire_skill_isCoolDown)
        {
            FireSkill.fillAmount -= 1 / fire_skill_cooldown * Time.deltaTime;
            if (FireSkill.fillAmount <= 0)
            {
                FireSkill.fillAmount = 0;
                fire_skill_isCoolDown = false;

            }

        }
    }
    void Moon_Skill()
    {
        if (playerAttack.isUsingMoonSkill && moon_skill_isCoolDown == false)
        {
            moon_skill_isCoolDown = true;
            MoonSkill.fillAmount = 1;
        }

        if (moon_skill_isCoolDown)
        {
            MoonSkill.fillAmount -= 1 / moon_skill_cooldown * Time.deltaTime;
            if (MoonSkill.fillAmount <= 0)
            {

                MoonSkill.fillAmount = 0;
                moon_skill_isCoolDown = false;

            }

        }
    }
    void Shiroinu_skill()
    {
        if (Input.GetKeyDown(KeyCode.C) && shiroinu_isCoolDown == false)
        {
            shiroinu_isCoolDown = true;
            Shiroinu.fillAmount = 1;
        }

        if (shiroinu_isCoolDown)
        {
            Shiroinu.fillAmount -= 1 / shiroinu_cooldown * Time.deltaTime;
            if (Shiroinu.fillAmount <= 0)
            {
                Shiroinu.fillAmount = 0;
                shiroinu_isCoolDown = false;

            }

        }

    }
    void Kuroinu_skill()
    {
        if (Input.GetKeyDown(KeyCode.V) && kuroinu_isCoolDown == false)
        {
            kuroinu_isCoolDown = true;
            Kuroinu.fillAmount = 1;
        }

        if (kuroinu_isCoolDown)
        {
            Kuroinu.fillAmount -= 1 / kuroinu_cooldown * Time.deltaTime;
            if (Kuroinu.fillAmount <= 0)
            {
                Kuroinu.fillAmount = 0;
                kuroinu_isCoolDown = false;

            }

        }

    }
}
