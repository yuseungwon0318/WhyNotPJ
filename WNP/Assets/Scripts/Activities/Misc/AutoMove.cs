using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public float speed = 3f;
    public Vector2 moveDir;

	private void Start()
	{
		moveDir = moveDir.normalized;
	}

	private void Update()
	{
		transform.Translate(moveDir * speed * Time.deltaTime, Space.Self);
	}
}
