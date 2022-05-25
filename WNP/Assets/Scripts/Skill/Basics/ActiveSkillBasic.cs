using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

/// <summary>
/// ��Ƽ�� ��ų�� �ְ��ϴ� Ŭ����.
/// �̰��� ����ϴ� ���� ��� ��Ƽ�� ��ų��.
/// ���� ��ų�� ���ÿ� ����.
/// 
/// ��ų�� ��� ��밡���� �κ��� ����.
///   
/// �� ������ ��� (���� 1) : 
/// �� ���� ������ ���� �� ���� ���� ū �ʰ� ���� �ǵ��� ���Ѵ�.
/// �� ���� ������ ���� �� �� ������ ����Ѵ�. (����� �� �� �� �� ���� ���� ����)
/// ������ �ش� ������ ���� ���� �� �������� ã�´�.
/// ���ٸ� ���� ���� �Ҵ����ְ�, false�� ��ȯ�Ѵ�.
/// �ִٸ� �ش� ���� �Ҵ����ְ�, true �� ��ȯ�Ѵ�.
/// </summary>

public abstract class ActiveSkillBasic : MonoBehaviour, IUsable, IColored//��� ��Ƽ�� ��ų�� ���� ��.
{
	public SkillBasic.Skills SkillInfo; //skillinfo. �빮�� i
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
		//���⼭ ���?
		IColored.ColorCodes? newColor = ColorBasic.AllColors.Find(item => item.colorRatio == newColorRatio).colorCodes;
		if(baseColor == IColored.ColorCodes.Black || newColor == null) //�������� ���� �� ���� �� ����. ������ ������ ���� �������� �ʴ´ٸ� ���� �� ����.
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


