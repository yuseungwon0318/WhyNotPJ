using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
/// <summary>
/// ������ ��Ŀ���� ����. ���� �����ϸ� �������� ����.
/// skillroot Ȯ����Ű��.
/// �ڷᱸ�� �����غ���.
/// ��ų ��� ���� ����
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
