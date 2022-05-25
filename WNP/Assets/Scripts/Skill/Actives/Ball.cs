using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Ball : ActiveSkillBasic
{
	AutoMove ESphere;

	public override void Init()
	{
		SkillInfo = SkillBasic.AllSkills[((int)SkillBasic.SkillCodes.Ball)];
		ColorInfo = ColorBasic.AllColors[((int)IColored.ColorCodes.White)];
		ESphere = Resources.Load<AutoMove>("Skill/Ball");
	}

	public override void Use()
	{																																																																																									
		Instantiate(ESphere, transform.position, Quaternion.identity);
	}
}