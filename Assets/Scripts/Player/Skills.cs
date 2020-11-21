using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills
{

    List<Skill> skills = new List<Skill>();
    public Skills()
    {
        AddSkill(new Skill(Skill.SkillType.fireSkill, true));
        AddSkill(new Skill(Skill.SkillType.moonSkill, false));
        AddSkill(new Skill(Skill.SkillType.windSkill, false));
        Debug.Log("This is the number of skills added:" + skills.Count);

    }
    public void AddSkill(Skill skill)
    {
        skills.Add(skill);
    }
    public List<Skill> GetSkillList()
    {
        return skills;
    }

}
