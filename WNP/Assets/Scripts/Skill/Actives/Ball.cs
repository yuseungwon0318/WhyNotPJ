using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Ball : ActiveSkillBasic //���� ��� ������ ��ų�̱⿡ IUsable�� ���.
{
	public AutoMove ESphere;
	public override void Use()
	{
		Instantiate(ESphere, transform.position, Quaternion.identity);
	}
}