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
/// </summary>

public abstract class ActiveSkillBasic : ColorBasic//��� ��Ƽ�� ��ų�� ���� ��.
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

	public override void Use()
	{

	}
}


