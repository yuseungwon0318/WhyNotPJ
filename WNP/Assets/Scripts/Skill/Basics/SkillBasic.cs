using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
/// <summary>
/// ��ų�� �⺻ Ŭ����
/// ��� ��ų�� �̰��� �����.
/// ��Ƽ��/�нú� ��ų�� ���� ����.
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
				Debug.Log("Ȯ��");
	}
	public struct Skills
	{
		public string skillName { get; private set; }
		public SkillCodes skillCode { get; private set; }
		public float cooldown { get; private set; } //��ٿ��� ��Ÿ��. �ߵ����� �ʴ� �׽� ���� ��ų ���� 0�� ������ ������.

		public Skills(SkillCodes code)
		{
			Init();
			skillName = skillCodeNamePair[code];
			skillCode = code;
			cooldown = skillCodeCooldownPair[code];

		}
	}
}
