using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreFloor : MonoBehaviour
{
	public int ignoreLayer = 7;
	public PlatformEffector2D myPlat;
	private void Start()
	{
		myPlat = GetComponent<PlatformEffector2D>();
	}
	private void Update()
	{
		if (PlayerController.Instance.fallchanged)
		{
			if (PlayerController.Instance.isFall)
			{
				PlatformIgnore();
			}
			else
			{
				PlatformRecover();
			}
		}

	}
	public void PlatformIgnore()
	{
		myPlat.colliderMask &= ~(1 << ignoreLayer);
	}
	public void PlatformRecover()
	{
		myPlat.colliderMask |= 1 << ignoreLayer;
	}
}
