using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Fallable"))
		{
			PlayerController.Instance.isGrounded = true;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Fallable"))
		{
			PlayerController.Instance.isGrounded = false;
		}
	}
}
