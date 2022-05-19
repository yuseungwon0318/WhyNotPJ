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
///   > ���� ��� �ذ�� ã��
///   
/// �� ������ ��� (���� 1) : 
/// �� ���� ������ ���� �� ���� ���� ū �ʰ� ���� �ǵ��� ���Ѵ�.
/// �� ���� ������ ���� �� �� ������ ����Ѵ�.
/// ������ �ش� ������ ���� ���� �� �������� ã�´�.
/// ���ٸ� null�� ��ȯ�Ѵ�. (�Ұ������� ����Ͽ� )
/// �ִٸ� �ش� ���� ��ȯ�Ѵ�.
/// </summary>

public abstract class ActiveSkillBasic : MonoBehaviour, IUsable, IColored//��� ��Ƽ�� ��ų�� ���� ��.
{
	public string Name { get => Name; protected set => Name = value;} //��ų �̸�.
	public SkillBasic.SkillCodes SkillCode { get => SkillCode; protected set => SkillCode = value;} //��ų �ڵ�
	public IColored.ColorCodes ColorCode { get; set;} //��ų�� �� ���� �ڵ�
	public string ColorDesc { get; set;} //���򺰷� ���� ����, ���λ� ������.

	public void Init(SkillBasic.SkillCodes skillCode, IColored.ColorCodes colorCode) //�ʱ�ȭ
	{
		//Name = skillCodeNamePair[skillCode];
		SkillCode = skillCode;
		ColorCode = colorCode;
		//ColorDesc = colorCodeDescPair[colorCode];
	}

	public virtual void Use()
	{
		Debug.Log("��ų ȣ���.");
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
		//���⼭ ���?
		IColored.ColorCodes? newColor = ColorBasic.allColors.Find(item => item.colorRatio == newColorRatio).colorCodes;
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


