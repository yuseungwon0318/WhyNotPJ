using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerFeet : MonoBehaviour
{
	public float rad = 0.3f;
	public LayerMask ignoreLayer;
	public PlayerController pc;
	Collider2D feetCol;
	private void Start()
	{
		ignoreLayer = ~ignoreLayer;
	}
	private void Update()
	{
		
		feetCol = Physics2D.OverlapCapsule(transform.position, new Vector2(1,1), CapsuleDirection2D.Horizontal,0, ignoreLayer);
		if (!feetCol)
		{
			pc.isGrounded = false;
		}
		else if ((feetCol.CompareTag("Ground") || feetCol.CompareTag("Fallable")) && Mathf.Approximately(pc.rig.velocity.y, 0))
		{
			pc.isGrounded = true;
		}
		else
		{
			pc.isGrounded = false;
		}
	}
	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(transform.position, rad);
	}
}
