using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
/// <summary>
/// 스킬의 기본 클래스
/// 모든 스킬은 이것을 상속함.
/// 액티브/패시브 스킬이 있을 예정.
/// </summary>
public class SkillBasic
{
    
	public enum SkillCodes
	{
		None = -1,
		Ball,
		Smash
	}
	public Dictionary<SkillCodes, string> skillCodeNamePair = new Dictionary<SkillCodes, string>()
	{
		{SkillCodes.Ball, "Ball" },
		{SkillCodes.Smash, "Smash" },
	};
	
}
