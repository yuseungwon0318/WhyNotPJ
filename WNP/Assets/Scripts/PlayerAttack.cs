using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 아무 조작이 없을 때 공격 시 마우스 입력 안 됨
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    public float playerDamage = 0.2f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButton(0) && collision.gameObject.layer == 8)
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
