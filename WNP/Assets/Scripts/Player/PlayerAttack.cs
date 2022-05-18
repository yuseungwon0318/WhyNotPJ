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
    public float playerAttackPower = 0.2f;
    public bool isAttack;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (PlayerController.Instance.hor > 0)
        {
            Gizmos.DrawWireCube(new Vector2(transform.position.x + 1, transform.position.y), size);
        }
        if (PlayerController.Instance.hor < 0)
        {
            Gizmos.DrawWireCube(new Vector2(transform.position.x - 1, transform.position.y), size);
        }
        else if (PlayerController.Instance.hor == 0)
        {
            Gizmos.DrawWireCube(transform.position, size);
        }
    }
    private void Update()
    {
        if (PlayerController.Instance.hor > 0)
        {
            attack = Physics2D.OverlapBox(new Vector2(transform.position.x + 1, transform.position.y), size, 0, layer);
        }
        if (PlayerController.Instance.hor < 0)
        {
            attack = Physics2D.OverlapBox(new Vector2(transform.position.x - 1, transform.position.y), size, 0, layer);
        }
        else if (PlayerController.Instance.hor == 0)
        {
            attack = Physics2D.OverlapBox(transform.position, size, 0, layer);
        }
    }

    void Attack()
    {
        IEnemyInterface IEnm = attack.GetComponent<IEnemyInterface>();
        if (IEnm != null && isAttack == true)
        {
            IEnm.Damage(playerAttackPower);
        }
    }
}
