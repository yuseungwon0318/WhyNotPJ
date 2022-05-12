using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �ڷ�ƾ���� ���� ��Ÿ�� ����� Larva ���� ��Ÿ�ӵ� �ڷ�ƾ���� ���� ����
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    Collider2D attack;
    [SerializeField] private Vector2 size;
    [SerializeField] private LayerMask layer;
    public float attackPower = 0.2f;
    public bool isAttack = false;
    bool onLeft = false;
    bool onRight = false;

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

        Attack();
    }

    void Attack()
    {
        if (attack.gameObject.layer == 11)
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }

        if (Input.GetMouseButton(0) && isAttack == true)
        {
            attack.GetComponent<Larva>().enemyHp -= attackPower;
        }
    }
}
