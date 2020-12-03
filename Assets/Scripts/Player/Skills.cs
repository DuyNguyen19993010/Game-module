using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills
{

    List<Skill> skills = new List<Skill>();
    public Skills()
    {
        AddSkill(new Skill(Skill.SkillType.fireSkill, false));
        AddSkill(new Skill(Skill.SkillType.moonSkill, false));
        AddSkill(new Skill(Skill.SkillType.shiroinuSkill, false));
        AddSkill(new Skill(Skill.SkillType.kuroinuSkill, false));
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
