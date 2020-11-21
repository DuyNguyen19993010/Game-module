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
    bool fire_skill_isCoolDown = false;
    //---------------------------Calculate and render the cool down for each skill----------------
    [Header("Moon Skill")]
    public Image MoonSkill;
    private float moon_skill_cooldown;
    bool moon_skill_isCoolDown = false;


    void Start()
    {
        refresh = false;
        playerAttack = GameObject.Find("Player").gameObject.GetComponent<Attacks>();
        playerStat = GameObject.Find("Player").gameObject.GetComponent<PlayerStat>();
        //-----------------Setting fire skill attribute------------------------
        fire_skill_cooldown = playerAttack.fire_skill_cooldown;
        FireSkill.fillAmount = 0;
        //-----------------Setting moon skill attribute------------------------
        moon_skill_cooldown = playerAttack.moon_skill_cooldown;
        MoonSkill.fillAmount = 0;
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
        }
    }
    public void Refresh()
    {
        FireSkill.fillAmount = 0;
        MoonSkill.fillAmount = 0;
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
}
