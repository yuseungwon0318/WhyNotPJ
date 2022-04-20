using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Vector3 dir;
    Animator animator;
    SpriteRenderer spriteRenderer;
    GameObject enemyDetectRange;
    GameObject target;
    public GameObject enemyDetectRangePut;
    public float speed = 2;

    void Start()
    {
        target = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyDetectRange = transform.GetChild(0).gameObject;

        if (target.transform.position.x >= transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void Update()
    {
        dir = target.transform.position - transform.position;

        dir.Normalize();

        if (enemyDetectRange.GetComponent<EnemyDetectRange>().detected == true)
        {
            if (target.transform.position.x >= transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }

            transform.position += dir * speed * Time.deltaTime;

            animator.SetBool("Monster_larva_move", true);
        }
        else
        {
            animator.SetBool("Monster_larva_move", false);
        }
    }
}
