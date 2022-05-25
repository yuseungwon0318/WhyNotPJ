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
/// 
/// 스킬의 경우 사용가능한 부분을 가짐.
///   
/// 색 섞기의 경우 (버전 1) : 
/// 두 색의 차수를 얻은 뒤 작은 쪽이 큰 쪽과 같게 되도록 곱한다.
/// 두 색의 비율을 더한 뒤 그 비율을 약분한다. (약분을 할 지 안 할 지에 대해 생각)
/// 비율이 해당 비율과 같은 색을 색 사전에서 찾는다.
/// 없다면 최초 색을 할당해주고, false를 반환한다.
/// 있다면 해당 색을 할당해주고, true 를 반환한다.
/// </summary>

public abstract class ActiveSkillBasic : MonoBehaviour, IUsable, IColored//모든 액티브 스킬엔 색이 들어감.
{
	public SkillBasic.Skills SkillInfo; //skillinfo. 대문자 i
	public ColorBasic.Colors ColorInfo;
	public abstract void Init();
	public abstract void Use();

	public bool MixColor(IColored.ColorCodes baseColor, IColored.ColorCodes catalyst, out IColored.ColorCodes result)
	{ 
		int multipler;
		Vector3 newColorRatio;
		if(ColorBasic.AllColors[(int)baseColor].colorDimention > ColorBasic.AllColors[(int)catalyst].colorDimention)
		{
			multipler = ColorBasic.AllColors[(int)baseColor].colorDimention/ ColorBasic.AllColors[(int)catalyst].colorDimention;
			newColorRatio = ColorBasic.AllColors[(int)baseColor].colorRatio + (ColorBasic.AllColors[(int)catalyst].colorRatio * multipler);
		}
		else
		{
			multipler = ColorBasic.AllColors[(int)catalyst].colorDimention / ColorBasic.AllColors[(int)baseColor].colorDimention;
			newColorRatio = ColorBasic.AllColors[(int)catalyst].colorRatio + ColorBasic.AllColors[(int)baseColor].colorRatio * multipler;
		}
		//여기서 약분?
		IColored.ColorCodes? newColor = ColorBasic.AllColors.Find(item => item.colorRatio == newColorRatio).colorCodes;
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


