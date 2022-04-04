using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreFloor : MonoBehaviour
{
	public int ignoreLayer = 7;
	public PlatformEffector2D myPlat;
	public PlayerController pc;
	private void Start()
	{
		myPlat = GetComponent<PlatformEffector2D>();
	}
	private void Update()
	{
		if (pc.fallChanged)
		{
			if (pc.isFall)
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
