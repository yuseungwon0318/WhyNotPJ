using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Collider2D attack;
    Animator attackAnim;
    [SerializeField] private Vector2 size;
    [SerializeField] private LayerMask layer;
    [SerializeField] private bool isAttack = false;
    [SerializeField] private float attackPower;
    [SerializeField] private float attackTime;
    
    private bool onLeft = false;
    private bool onRight = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (PlayerController.Instance.hor > 0 || (PlayerController.Instance.hor == 0 && onRight == true))
        {
            Gizmos.DrawWireCube(new Vector2(transform.position.x + 1, transform.position.y), size);
        }
        if (PlayerController.Instance.hor < 0 || (PlayerController.Instance.hor == 0 && onLeft == true))
        {
            Gizmos.DrawWireCube(new Vector2(transform.position.x - 1, transform.position.y), size);
        }
    }


	private void Awake()
	{
		attackAnim = GetComponent<Animator>();
	}
	private void Update()
    {
        if (PlayerController.Instance.hor > 0 || (PlayerController.Instance.hor == 0 && onRight == true))
        {
            attack = Physics2D.OverlapBox(new Vector2(transform.position.x + 1, transform.position.y), size, 0, layer);
            onLeft = false;
            onRight = true;
        }
        if (PlayerController.Instance.hor < 0 || (PlayerController.Instance.hor == 0 && onLeft == true))
        {
            attack = Physics2D.OverlapBox(new Vector2(transform.position.x - 1, transform.position.y), size, 0, layer);
            onLeft = true;
            onRight = false;
        }

        if(Input.GetMouseButtonDown(0) && !isAttack)
		{
            attackAnim.SetTrigger("IsAttack");
        }

        if (attack && Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        if ((attack.gameObject.layer == 11 ) && !isAttack )
        {
            
            isAttack = true;
            StartCoroutine(AttackDelay());
        }
    }

    IEnumerator AttackDelay()
    {
        attack.GetComponent<Larva>().healthPoint -= attackPower;
        yield return new WaitForSeconds(attackTime);
        isAttack = false;
    }
}
