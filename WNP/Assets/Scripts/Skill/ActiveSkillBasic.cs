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
		int multipler;
		Vector3 newColorRatio;
		if(ColorBasic.allColors[(int)baseColor].colorDimention > ColorBasic.allColors[(int)catalyst].colorDimention)
		{
			multipler = ColorBasic.allColors[(int)baseColor].colorDimention/ ColorBasic.allColors[(int)catalyst].colorDimention;
			newColorRatio = ColorBasic.allColors[(int)baseColor].colorRatio + (ColorBasic.allColors[(int)catalyst].colorRatio * multipler);
		}
		else
		{
			multipler = ColorBasic.allColors[(int)catalyst].colorDimention / ColorBasic.allColors[(int)baseColor].colorDimention;
			newColorRatio = ColorBasic.allColors[(int)catalyst].colorRatio + ColorBasic.allColors[(int)baseColor].colorRatio * multipler;
		}
		//여기서 약분?
		IColored.ColorCodes? newColor = ColorBasic.allColors.Find(item => item.colorRatio == newColorRatio).colorCodes;
		if(baseColor == IColored.ColorCodes.Black || newColor == null) //검은색은 색을 더 섞을 수 없음. 동일한 비율의 색이 존재하지 않는다면 섞을 수 없음.
		{
			result = baseColor;
			return false;
		}

		result = (IColored.ColorCodes)newColor;

		return true;
	}
	public IColored.ColorCodes Replace(IColored.ColorCodes change)
	{
		return change;
	}

}


