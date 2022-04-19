using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
