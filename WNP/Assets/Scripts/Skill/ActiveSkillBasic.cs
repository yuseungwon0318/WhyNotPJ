using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

/// <summary>
/// 액티브 스킬을 주관하는 클래스.
/// 이것을 상속하는 것은 모두 액티브 스킬임.
/// 색과 스킬을 동시에 가짐.
///   > 다중 상속 해결법 찾기
/// </summary>

public abstract class ActiveSkillBasic : ColorBasic//모든 액티브 스킬엔 색이 들어감.
{
	public string Name { get => Name; protected set => Name = value;} //스킬 이름.
	public SkillBasic.SkillCodes SkillCode { get => SkillCode; protected set => SkillCode = value;} //스킬 코드
	public IColored.ColorCodes ColorCode { get; set;} //스킬에 들어갈 색깔 코드
	public string ColorDesc { get; set;} //색깔별로 붙을 설명, 접두사 같은거.

	public void Init(SkillBasic.SkillCodes skillCode, IColored.ColorCodes colorCode) //초기화
	{
		//Name = skillCodeNamePair[skillCode];
		SkillCode = skillCode;
		ColorCode = colorCode;
		//ColorDesc = colorCodeDescPair[colorCode];
	}

	public override void Use()
	{

	}
}


