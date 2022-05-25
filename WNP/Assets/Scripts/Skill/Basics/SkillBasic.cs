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

	

	public static List<Skills> AllSkills = new List<Skills>()
	{
		new Skills(SkillCodes.Ball),
		new Skills(SkillCodes.Smash),
	};

	public enum SkillCodes
	{
		None = -1,
		Ball,
		Smash,

		Max
	}
	static Dictionary<SkillCodes, string> skillCodeNamePair = new Dictionary<SkillCodes, string>()
	{
		{SkillCodes.Ball, "Ball" },
		{SkillCodes.Smash, "Smash" },
	};
	static Dictionary<SkillCodes, float> skillCodeCooldownPair = new Dictionary<SkillCodes, float>() 
	{
		{SkillCodes.Ball, 1.0f },
		{SkillCodes.Smash, 2.0f },
	};
	protected static void Init()
	{
		if(skillCodeNamePair != null)
			if(skillCodeCooldownPair != null)
				Debug.Log("확인");
	}
	public struct Skills
	{
		public string skillName { get; private set; }
		public SkillCodes skillCode { get; private set; }
		public float cooldown { get; private set; } //쿨다운을 나타냄. 발동하지 않는 항시 지속 스킬 등은 0을 값으로 가진다.

		public Skills(SkillCodes code)
		{
			Init();
			skillName = skillCodeNamePair[code];
			skillCode = code;
			cooldown = skillCodeCooldownPair[code];

		}
	}
}
