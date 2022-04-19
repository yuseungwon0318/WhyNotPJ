using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerFeet : MonoBehaviour
{
	public float rad = 0.7f;
	public LayerMask ignoreLayer;
	Collider2D feetCol;
	private void Start()
	{
		ignoreLayer = ~ignoreLayer;
	}
	private void Update()
	{
		
		feetCol = Physics2D.OverlapCircle(transform.position, 0.7f, ignoreLayer);
		if (!feetCol)
		{
			PlayerController.isGrounded = false;
		}
		else if (feetCol.CompareTag("Ground") || feetCol.CompareTag("Fallable"))
		{
			PlayerController.isGrounded = true;
		}
		else
		{
			PlayerController.isGrounded = false;
		}
	}
	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(transform.position, 0.7f);
	}
}
