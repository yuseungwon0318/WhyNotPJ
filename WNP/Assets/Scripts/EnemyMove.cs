using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Vector3 dir;
    Animator animator;
    SpriteRenderer spriteRenderer;
    GameObject enemyDetectRangeLeft;
    GameObject enemyDetectRangeRight;
    GameObject target;
    public GameObject enemyDetectRangeLeftPut;
    public GameObject enemyDetectRangeRightPut;
    public float speed = 2;

    void Start()
    {
        target = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyDetectRangeLeft = Instantiate(enemyDetectRangeLeftPut);
        enemyDetectRangeRight = Instantiate(enemyDetectRangeRightPut);
    }

    void Update()
    {
        dir = target.transform.position - transform.position;
        enemyDetectRangeLeft.transform.position = transform.position;
        enemyDetectRangeRight.transform.position = transform.position;

        dir.Normalize();

        if (enemyDetectRangeLeft.GetComponent<EnemyDetectRange>().detected == true)
        {
            spriteRenderer.flipX = false;

            transform.position += dir * speed * Time.deltaTime;

            animator.SetBool("Monster_larva_move", true);
        }
        else if (enemyDetectRangeRight.GetComponent<EnemyDetectRange>().detected == true)
        {
            spriteRenderer.flipX = true;

            transform.position += dir * speed * Time.deltaTime;

            animator.SetBool("Monster_larva_move", true);
        }
        else
        {
            animator.SetBool("Monster_larva_move", false);
        }
    }
}
