using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreFloor : MonoBehaviour
{
	public static IgnoreFloor ignoreFloor;
	public int ignoreLayer = 7;
	PlatformEffector2D myPlat;
	private void Start()
	{
		myPlat = GetComponent<PlatformEffector2D>();
		ignoreFloor = GetComponent<IgnoreFloor>();
	}
	public static void PlatformIgnore()
	{
		ignoreFloor.myPlat.colliderMask  &= ~(1 << ignoreFloor.ignoreLayer);
	}
	public static void PlatformRecover()
	{
		ignoreFloor.myPlat.colliderMask |= 1 << ignoreFloor.ignoreLayer;
	}
}
