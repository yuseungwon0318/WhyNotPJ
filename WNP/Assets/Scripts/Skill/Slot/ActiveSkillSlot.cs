using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkillSlot
{
    public bool isActive = false;
    ActiveSkillBasic currentSkill;
    public delegate void UseSkill();
    public UseSkill Use;

    public void Init(ActiveSkillBasic skill)
	{
        skill.Init();
        currentSkill = skill;
        Use = skill.Use;
	}
}
