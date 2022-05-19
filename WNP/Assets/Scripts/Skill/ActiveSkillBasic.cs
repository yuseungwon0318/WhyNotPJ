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
/// 
/// �� �ռ��� ��� (���� 2) : 
/// �� ���� ������ ���� �� ���� ���ٸ� �ռ��� �����Ѵ�.
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
		IColored.ColorCodes resultColor = IColored.ColorCodes.None;
		if(baseColor == IColored.ColorCodes.Black ) //���⿡ ������ ���� ������ �ֱ�.
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
			//���⿡ ������ ���� ������ �ֱ�. �Ƹ��� ��ų�� ������ ���� ���� �ʿ��� ���̰�, ��ų�� Colors�� ���� ������ �� ��.
		{
			result = IColored.ColorCodes.Black;
			return false;
		}
		result = change;
		return true;
	}

}


