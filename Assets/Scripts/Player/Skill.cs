using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public enum SkillType
    {
        fireSkill,
        moonSkill,
        windSkill
    };
    public SkillType type;
    public bool enabled;

    public Skill(SkillType _type, bool _enabled)
    {
        this.type = _type;
        this.enabled = _enabled;
    }
}
