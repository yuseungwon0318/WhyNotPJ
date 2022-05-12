using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Slerf 기능 조사 예정
/// </summary>
public class Larva : MonoBehaviour
{
    Vector3 dir;
    GameObject target;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Collider2D detect;
    Collider2D attack;
    public static Larva Instance = null;
    public float enemyHp = 1f;
    public float attackPower = 0.2f;
    [SerializeField] private bool isAttack;
    [SerializeField] private float speed = 2;
    [SerializeField] private Vector2 detectSize;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float currentAttackTime = 0;
    [SerializeField] private float attackTime = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        target = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (target.transform.position.x >= transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, detectSize);
        Gizmos.DrawWireCube(transform.position, attackSize);
    }

    void Update()
    {
        dir = target.transform.position - transform.position;
        detect = Physics2D.OverlapBox(transform.position, detectSize, 0, layer);
        attack = Physics2D.OverlapBox(transform.position, attackSize, 0, layer);

        dir.Normalize();

        currentAttackTime += Time.deltaTime;

        if (enemyHp < 0.001)
        {
            Destroy(gameObject);
        }

        EnemyDetect();
        EnemyAttack();
    }

    private void EnemyDetect()
    {
        if (!attack && detect)
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

    private void EnemyAttack()
    {
        if (attack.gameObject.layer == 7)
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }

        if (isAttack == true)
        {
            if (currentAttackTime > attackTime)
            {
                target.GetComponent<PlayerController>().playerHp -= attackPower;

                currentAttackTime = 0;
            }
        }
    }
}
