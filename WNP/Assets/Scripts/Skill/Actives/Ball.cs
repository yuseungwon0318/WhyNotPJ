using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Ball : ActiveSkillBasic //볼은 사용 가능한 스킬이기에 IUsable을 상속.
{
	public AutoMove ESphere;

	public void Awake()
	{
		ESphere = Resources.Load<AutoMove>("Skill/Ball");
	}

	public override void Use()
	{
		Instantiate(ESphere, transform.position, Quaternion.identity);
	}
}