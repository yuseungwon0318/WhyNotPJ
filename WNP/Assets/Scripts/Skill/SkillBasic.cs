using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
/// <summary>
/// 색조합 메커니즘 구상. 조건 도배하면 간략하지 못함.
/// skillroot 확정시키기.
/// 자료구조 생각해보기.
/// 스킬 사용 구현 대충
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
