using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public abstract class Skills : SkillBasic, ColorBasic
{
	public string Name { get => Name; protected set => Name = value;}
	public SkillCodes SkillCode { get => SkillCode; protected set => SkillCode = value;}
	public ColorCodes ColorCode { get; set;}
	public string ColorDesc { get; set;}

	public void Init(SkillCodes skillCode, ColorCodes colorCode)
	{
		Name = skillCodeNamePair[skillCode];
		SkillCode = skillCode;
		ColorCode = colorCode;
		ColorDesc = colorCodeDescPair[colorCode];
	}
}


