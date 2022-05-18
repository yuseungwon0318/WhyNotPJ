using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 공격 도착 지점 고정하기
/// 애니메이션 타이밍 조정하기
/// 스킬 게이지 4 더해지는 거 수정
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
    [SerializeField] private float attackTime;

    public float initialAngle;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        target = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();

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

        EnemyDetect();
        EnemyAttack();
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
            curSkillGauge += 0.01f * Time.deltaTime;

            if (curSkillGauge < maxSkillGauge && isAttack == false)
            {
                isAttack = true;
                StartCoroutine(NormalAttackDelay());

            }
        }

        if (curSkillGauge >= maxSkillGauge)
        {
            isAttack = true;
            StartCoroutine(Attack());
        }
    }

    IEnumerator NormalAttackDelay()
    {
        animator.SetTrigger("NormalAttack");
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Move");
        animator.ResetTrigger("Attack");
        target.GetComponent<PlayerController>().playerHp -= attackPower;
        curSkillGauge += 2f;
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

        StartCoroutine(GetVelocity(transform.position, initialAngle));

        target.GetComponent<PlayerController>().playerHp -= attackPower * 10;
        curSkillGauge = 0;
        isAttack = false;
    }

    IEnumerator GetVelocity(Vector3 player, float initialAngle)
    {
        Vector3 targetV = target.transform.position;

        animator.SetTrigger("Attack");
        animator.ResetTrigger("AttackPrepare");

        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(targetV.x, 0, targetV.z);
        Vector3 planarPosition = new Vector3(player.x, 0, player.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = player.y - targetV.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (targetV.x > player.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        rig.velocity = finalVelocity;

        yield return new WaitForSeconds(0);
    }
}
