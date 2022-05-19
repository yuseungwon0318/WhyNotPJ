using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerFeet : MonoBehaviour
{
	public float rad = 0.3f;
	public LayerMask ignoreLayer;
	List<Collider2D>  feetCol;
	private void Start()
	{
		ignoreLayer = ~ignoreLayer;
	}
	private void Update()
	{
		
		feetCol = 
			Physics2D.OverlapCapsuleAll(transform.position, new Vector2(1,1f), CapsuleDirection2D.Horizontal,0, ignoreLayer).ToList();
		if (feetCol == null)
		{
			PlayerController.Instance.isGrounded = false;
		}
		else if (feetCol.Exists(item => {return item.CompareTag("Fallable") || item.CompareTag("Ground");} ) && Approximate(PlayerController.Instance.rig.velocity.y, 0, 0.2f))
		{
			PlayerController.Instance.isGrounded = true;
		}
		else
		{
			PlayerController.Instance.isGrounded = false;
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
