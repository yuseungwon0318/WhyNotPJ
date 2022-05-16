using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Slerp 정상 작동하게 만들기
/// 애니메이션 부여해서 공격 준비 시간 만들기
/// 평타 및 스킬 애니메이션 넣기
/// </summary>
public class Larva : MonoBehaviour
{
    Vector3 dir;
    GameObject target;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Collider2D detect;
    Collider2D attack;
    public static Larva Instance;
    public float healthPoint;
    [SerializeField] private bool isAttack;
    [SerializeField] private float speed;
    [SerializeField] private float attackPower;
    [SerializeField] private float curSkillGauge;
    [SerializeField] private float maxSkillGauge;
    [SerializeField] private Vector2 detectSize;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float curAttackTime = 0;
    [SerializeField] private float attackTime;

    private void Awake()
    {
        Instance = this;
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

        curAttackTime += Time.deltaTime;

        if (healthPoint < 0.001)
        {
            Destroy(gameObject);
        }

        EnemyDetect();

        if (attack)
        {
            EnemyAttack();
        }
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

            if (curSkillGauge < maxSkillGauge)
            {
                transform.position += dir * speed * Time.deltaTime;
                curSkillGauge += 0.01f * Time.deltaTime;
            }
            else
            {
                transform.position = Vector3.Slerp(transform.position, target.transform.position, 0.05f);

                if (attack.gameObject.layer == 7)
                {
                    curSkillGauge = 0;
                }
            }

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
            if (curAttackTime > attackTime)
            {
                target.GetComponent<PlayerController>().playerHp -= attackPower;

                curSkillGauge += 2f;

                curAttackTime = 0;
            }
        }
    }
}
