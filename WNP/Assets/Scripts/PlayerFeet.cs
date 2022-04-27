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
		
		feetCol = Physics2D.OverlapCapsule(transform.position, new Vector2(1,1f), CapsuleDirection2D.Horizontal,0, ignoreLayer);
		if (!feetCol)
		{
			pc.isGrounded = false;
		}
		else if ((feetCol.CompareTag("Ground") || feetCol.CompareTag("Fallable")) && Approximate(pc.rig.velocity.y, 0, 0.2f))
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
		Gizmos.DrawWireSphere(transform.position, rad);
	}
	public static bool Approximate(float a, float b, float range)
	{
		if(a > b - range && a < b + range)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
