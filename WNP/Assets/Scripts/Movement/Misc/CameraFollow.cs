using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
	private void FixedUpdate()
	{
		transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
		//z좌표는 유지
	}
}
