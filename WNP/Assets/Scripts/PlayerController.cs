using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// 
/// ���� ���� ���� �Ϸ�
/// ���� �������ִٰ��� ������ �� ����.
/// �Ʒ�Ű�� ������ �ӵ��� ���� ���ϴ� �������
/// �浹������ Ȱ��ȭ���Ѽ�
/// �����̽��� ������ �������Բ� ��.
/// 
/// �׸��� S�����̽��� �浹������ ��� ��ȿȭ��ų�� ����.
/// ������ 1���ε� ���߿� �����߻��ϸ�
/// ���� ���̰� �ʹ� �����ؼ� �߻��� �������״�
/// �����̸� �ٲٰų� �ð��� ���� �����ϸ� �ɵ�.
/// 
/// coroutine�� ���� ����.
/// �Ʒ� IEnumerator��� �����ִ� �Լ��� �ڷ�ƾ�̴�.
/// �ڷ�ƾ�� �Ϲ� �Լ��ʹ� �ٸ��� ���ϰ��� ������ �� �� �ִµ�,
/// �̰͵� �׳� ���ϰ��� �ƴϰ� Ư�� ����� �ϵ��� ���Ͻ�ų�� �ִ�.(yield return)
/// �ַ� ���Ǵ°� �ð�(��)��⳪, �Է��� ���ö� ���� ��ٸ��� ���̴�.
/// �ڷ�ƾ�� ����ϴ� ������ �����ؼ��� ������ ����ó���� �����ؼ��� ���� ũ��.
/// 
/// collidermask�� ���� ����,
/// ��ǻ�� ���� ���⿡�� ���α׷��Ӹ�� �ִµ�
/// �װſ���� ���ذ� ����.
/// 
/// ������� ���̾� 0~10�� �ִٰ� �ĺ��ڴ�.
/// �׷��ٸ� ��ǻ�ʹ� �̸� 2������ �����ϱ� ������ 00 0000 0000���� ������.
/// �̶�, �浹������ �����ϰڴٴ� ��ȣ�� 0, �浹�� �ϰڴٴ� ��ȣ�� 1�̴�.
/// �׷��⶧���� ��Ʈ����Ʈ�� ������ ��ŭ �о �ݴ�� ���ָ� �۵��ϰ� �ȴ�.
/// 2������ ���������� �Ӹ��ӿ� �Ϻ��� ������ ���⿡ ��Ʈ����Ʈ ���� ���ڸ� �������� �ְ�����
/// �׷��� ���ذ� ��������� ��Ʈ����Ʈ�� �����ϵ��� ����.
/// ������ �ݴ�� ��Ȱ��ȭ
/// ��Ȱ��ȭ�� ���������� Ȱ��ȭ ��ų��ŭ �о
/// or�� ���ָ� �ȴ�.
/// 1�� �и� �޺κе� ���� 1�� ������ ���� �̿��� ������ ����.
/// ignoreFloor�� ���� �ڵ尡 �ְ�, ȣ��δ� playercontroller�� collisionOn�̴�.
/// </summary>
public class PlayerController : MonoBehaviour
{
	#region public ����
	public float DashGap;
    public float speed;
    public float dashPower;
    [Tooltip("��� ���ӽð��� ��Ÿ��")]
    public float defaultTime;
    public float jumpPower;
    public GameObject parSpawner;
    public Transform player;
    #endregion
    #region private ������Ʈ
    Rigidbody2D rig;
    #endregion
    #region ��ð��� ������
    Vector2 v;
	float keyTime;
    bool ADash = false;
    bool DDash = false;
    float dashTime;
    float declinedDashSpeed = 1f; //��� ���ӵ� ���ҷ� (�����)
    float defaultSpeed;
	bool firstKeyPressedA = false;
    bool firstKeyPressedD = false;
    bool resetA = false;
    bool resetD = false;
	#endregion
	#region ����/���ϰ��� ������
	bool isJump = false;
    bool sPressed = false;
    bool spacePressed = false;
    public static bool isGrounded = false;
    public bool isFall = false;
    public bool fallchanged = false;
    #endregion
    #region �ִϸ��̼� ���� ����
    Animator animator;

    #endregion

    void Start()
    {
        defaultSpeed = speed;
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        if (hor > 0)
        {
            animator.SetTrigger("Right_run");
            animator.ResetTrigger("Left_run");
            animator.ResetTrigger("Right_idle");
            animator.ResetTrigger("Left_idle");

            parSpawner.transform.position = new Vector3(player.position.x + 1, player.position.y, player.position.z);
        }

        if (hor < 0)
        {
            animator.SetTrigger("Left_run");
            animator.ResetTrigger("Right_run");
            animator.ResetTrigger("Right_idle");
            animator.ResetTrigger("Left_idle");
            
            parSpawner.transform.position = new Vector3(player.position.x - 1, player.position.y, player.position.z);
        }
        
        if (hor == 0)
        {
            if (v.normalized == Vector2.left)
            {
                animator.SetTrigger("Left_idle");
                animator.ResetTrigger("Left_run");
                animator.ResetTrigger("Right_run");
            }
            if (v.normalized == Vector2.right)
            {
                animator.SetTrigger("Right_idle");
                animator.ResetTrigger("Left_run");
                animator.ResetTrigger("Right_run");
            }
        }
        else
        {
            animator.ResetTrigger("Right_idle");
            animator.ResetTrigger("Left_idle");
        }

        v = new Vector2(hor * defaultSpeed, rig.velocity.y);
        rig.velocity = v;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetMouseButtonDown(0))
		{
            rig.velocity +=Vector2.down * 0.001f;
		}

        DetectDash();
        Jump();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.S))
        {
            sPressed = true;
        }
        else
        {
            sPressed = false;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            spacePressed = true;
        }
        else
        {
            spacePressed = false;
        }
        Dash();
    }

    void Dash()
	{
		if (ADash)
		{
            DDash = false;
            rig.AddForce(Vector2.left * dashPower * declinedDashSpeed,ForceMode2D.Impulse);
            dashTime -= Time.deltaTime;
            declinedDashSpeed /= 1.1f;
            if (dashTime <= 0)
            {
                ADash = false;
                resetD = true;
                declinedDashSpeed = 1;
            }
        }
        else if (DDash)
		{
            ADash = false;
            rig.AddForce(Vector2.right * dashPower * declinedDashSpeed,ForceMode2D.Impulse);
            dashTime -= Time.deltaTime;
            declinedDashSpeed /= 1.1f;
            if (dashTime <= 0)
            {
                DDash = false;
                resetA = true;
                declinedDashSpeed = 1;
            }
        }
        
        
	}

    void DetectDash()
    {
        
        if (Input.GetKeyDown(KeyCode.A) && firstKeyPressedA)
        {
			
            if (Time.time < keyTime + DashGap)
            {
                ADash = true;
                dashTime = defaultTime;
            }
            resetA = true;
        }
        if (Input.GetKey(KeyCode.A) && !firstKeyPressedA)
        {
            firstKeyPressedA = true;
            keyTime = Time.time;
        }
		if (Input.GetKeyDown(KeyCode.A))
		{
            resetD = true;
		}
        

        if (Input.GetKeyDown(KeyCode.D) && firstKeyPressedD)
        {
            
            if (Time.time < keyTime + DashGap)
			{
                DDash = true;
                dashTime = defaultTime;
            }

            resetD = true;
        }
        if (Input.GetKey(KeyCode.D) && !firstKeyPressedD)
        {
            firstKeyPressedD = true;
            keyTime = Time.time;
        }
		if (Input.GetKeyDown(KeyCode.D))
		{
            resetA = true;
		}
		if (resetA)
		{
            firstKeyPressedA = false;
            resetA = false;
        }
		if (resetD)
		{
            firstKeyPressedD = false;
            resetD = false;
		}
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            if (!sPressed)
            {
                if (!isJump && isGrounded && rig.velocity.y <= 0)
                {
                    rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    isJump = true;
                }
            }
        }
    }

    IEnumerator CollisionOn()
	{
        fallchanged = true;
		isFall =true;
        yield return new WaitForSeconds(0.7f);
        isFall = false;
        yield return null; // 1������ ���
        fallchanged = false;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Fallable"))
        {
            isJump = false;
        }
    }
	private void OnCollisionStay2D(Collision2D collision)
	{
        if (sPressed && spacePressed )
        {
            StartCoroutine(CollisionOn());
        }
    }
	private void OnCollisionExit2D(Collision2D collision)
	{
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Fallable"))
        {
            isJump = true;
        }
    }

    void AnimationUpdate()
    {

    }
}
