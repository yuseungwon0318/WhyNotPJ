using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterEffectManager : MonoBehaviour
{
	ParticleSystemRenderer ae;
	private void Start()
	{
		ae = GetComponent<ParticleSystemRenderer>();
		ae.enabled = false;
	}
	public void Activate(float xRot)
	{
		StartCoroutine(AfterCtrl(xRot));
	}
	IEnumerator AfterCtrl(float xRot)
	{
		ae.flip = new Vector3(xRot,0,0);
		ae.enabled= true;
		yield return new WaitForSeconds(PlayerController.Instance.defaultTime * 1.17f);
		ae.enabled = false;
	}
}
