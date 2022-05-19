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
///   
/// 색 섞기의 경우 (버전 1) : 
/// 두 색의 차수를 얻은 뒤 작은 쪽이 큰 쪽과 같게 되도록 곱한다.
/// 두 색의 비율을 더한 뒤 그 비율을 약분한다.
/// 비율이 해당 비율과 같은 색을 색 사전에서 찾는다.
/// 없다면 null을 반환한다. (불가능으로 취급하여 )
/// 있다면 해당 색을 반환한다.
/// 
/// 색 합성의 경우 (버전 2) : 
/// 두 색의 차수를 얻은 뒤 만일 같다면 합성을 진행한다.
/// </summary>

public abstract class ActiveSkillBasic : MonoBehaviour, IUsable, IColored//모든 액티브 스킬엔 색이 들어감.
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

	public virtual void Use()
	{
		Debug.Log("스킬 호출됨.");
	}

	public bool MixColor(IColored.ColorCodes baseColor, IColored.ColorCodes catalyst, out IColored.ColorCodes result)
	{ 
		IColored.ColorCodes resultColor = IColored.ColorCodes.None;
		if(baseColor == IColored.ColorCodes.Black ) //여기에 버전에 따른 조건을 넣기.
		{
			result = baseColor;
			return false;
		}
			

		result = resultColor;

		return true;
	}
	public bool Replace(IColored.ColorCodes change, out IColored.ColorCodes result)
	{
		if(IColored.ColorCodes.Black != change) 
			//여기에 버전에 따른 조건을 넣기. 아마도 스킬의 차수를 비교할 것이 필요할 것이고, 스킬을 Colors와 같이 만들어야 할 듯.
		{
			result = IColored.ColorCodes.Black;
			return false;
		}
		result = change;
		return true;
	}

}


