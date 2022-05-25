using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 공격 도착 지점 고정하기
/// 애니메이션 타이밍 조정하기
/// 스킬 공격 재구현
/// 상태(State) 구현
/// </summary>
public class Larva : MonoBehaviour
{
    Vector3 dir;
    GameObject target;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Collider2D detect;
    Collider2D attack;
    Rigidbody2D rig;
    public static Larva Instance;
    public float healthPoint;
    [SerializeField] private Vector2 detectSize;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private LayerMask layer;
    [SerializeField] private bool isAttack;
    [SerializeField] private float speed;
    [SerializeField] private float attackPower;
    [SerializeField] private float curSkillGauge;
    [SerializeField] private float maxSkillGauge;
    [SerializeField] private float upSkillGauge;
    [SerializeField] private float attackTime;

    public enum State : short
    {
        Idle = 0,
        Move = 1,
        NormalAttack = 2,
        AttackPrepare = 3,
        Attack = 4
    }

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

        if (healthPoint < 0.001)
        {
            Destroy(gameObject);
        }

        if (curSkillGauge >= maxSkillGauge)
        {
            isAttack = true;
            StartCoroutine(Attack());
        }
        else
        {
            EnemyDetect();
            EnemyAttack();
        }
    }

    private void EnemyDetect()
    {
        if (target.transform.position.x >= transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (!attack && detect)
        {
            transform.position += dir * speed * Time.deltaTime;
            curSkillGauge += 0.01f * Time.deltaTime;

            animator.SetTrigger("Move");
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("NormalAttack");
            animator.ResetTrigger("Attack");
        }

        if (!attack && !detect)
        {
            animator.SetTrigger("Idle");
            animator.ResetTrigger("Move");
            animator.ResetTrigger("NormalAttack");
            animator.ResetTrigger("Attack");
        }
    }

    private void EnemyAttack()
    {
        if (attack && attack.gameObject.layer == 7)
        {
            curSkillGauge += upSkillGauge * Time.deltaTime;

            if (curSkillGauge < maxSkillGauge && isAttack == false)
            {
                isAttack = true;

                StartCoroutine(NormalAttackDelay());
            }
        }
    }

    IEnumerator NormalAttackDelay()
    {
        animator.SetTrigger("NormalAttack");
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Move");
        animator.ResetTrigger("Attack");

        target.GetComponent<PlayerController>().playerHp -= attackPower;
        curSkillGauge += upSkillGauge;

        yield return new WaitForSeconds(attackTime);

        isAttack = false;
    }

    IEnumerator Attack()
    {
        animator.SetTrigger("AttackPrepare");
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Move");
        animator.ResetTrigger("NormalAttack");

        yield return new WaitForSeconds(2);

        animator.SetTrigger("Attack");
        animator.ResetTrigger("AttackPrepare");

        Jump();

        target.GetComponent<PlayerController>().playerHp -= attackPower * 10;

        curSkillGauge = 0;

        yield return new WaitForSeconds(attackTime);

        isAttack = false;
    }

    private void Jump()
    {
        //애니메이션 커브로 구현 예정
    }
}
