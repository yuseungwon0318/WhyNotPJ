using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
/// <summary>
/// ���� �����ؾ��ϴ� ��Ȳ :
/// 
/// ��! ������ ����.
/// 
///
/// 
/// 
/// <�ذ��> 
/// P1*����⺮�� ��/�Ʒ������� ���� �� ������, ���Ʒ����� ���� ��� ����Ⱑ Ȱ��ȭ.
/// 
/// P2*��� �Ұ� ���� ���� ���, �ű⿡ ���� ���߿� ��.
/// 
/// P3*��� �ȼ��� ���� ���� ����. �ڸ����� ������ ���� �ִ�.
/// 
/// P4*�÷����� ����⺮�� ���ÿ� ���� ���¿���, �������� Ȱ��ȭ ��� ��ҵ�.
/// �׷��Ƿ� ���ӵ��� �߰��� ���缭 ����� ��� ����.
/// �÷����� �ٴ����� ġ�� ���ٰ�, �÷����� ������ �������� �Ϲݻ��·� ��ȯ��.
/// 
/// P5*��� ������Ʈ�� ����ִٰ� ������ �Ѿ�� ���� �ϴ÷� ��. (�Ȱ�ĥ�Ÿ� �������.)
/// ������ ��Ģ ������ �׷��ǵ�, �̵� ����� ���� �����ؾ��� �ʿ䰡 ����.
/// ���� �̵��� ���� ���Ͽ� �ӵ��� ���Ƿ� ��ȭ��Ű�鼭 �ϰ� �ִµ�,
/// �׷��⿡ �� ������ ���� ���� ������ٵ��� ���� ��׸� �̵��� ����.
/// 
/// P6*�÷��������� ����� �Ʒ����� �������� ��, �÷����� ��ġ��
/// ���� ��õ��� isJump�� ��Ȱ��ȭ��.
/// ������ ���������� ���� ���ϴ� ���� ���� ���� ���س��Ƽ� ����������,
/// ���Ŀ� �ָ��� ��Ȳ�� ���� ���� ����.
/// 
/// P7* ���� ����� �ȵǴµ�, ���� ���� �ٴڿ� �������� �������°� �������� ����.
/// ��ܿ� �ö� ���� ���߿� �߸� �ٽ� ������ ��������. ( ���� ���°� ������ )
/// 
/// P8*��� �� ���濡�� ������ �ȸ���.
/// Y�� �ӵ��� �־ �׷����� ���� �ִ�.
/// 
/// P9*��ܿ��� ������ �ִϸ��̼��� ���������� ��������.
/// ���ι������� �ӵ��� �������°� ������ �׷���.
/// 
/// 
/// S1*����ĳ��Ʈ�� ����� ��ü�� �ִ����� ���� ������ �Ǵ��ؼ� ���¸� ����.
/// S2*�߰� collider�� �ٿ��� �ذ�. raycast�� ����ϱ⿡ �� ���� ������.
/// S3*�ڸ� �� ���� �κ��� ���� ���� �߸�. ũ�⸦ 2�ȼ����� �ٿ��� �ذ�.
/// S4*�� �������� ���� �������� ���������� ���� �ʵ��� ��.
/// S5*��ܰ� �ٴ��� collider�� ������. ����� �� ª��, �ٴ��� �� ��� �ϸ� ��.
/// S6*���ǰ� Ÿ����.
/// S7*P3�ذ��� �ذ��.
/// S8*�÷��̾� ���� ���� �����̱⿡ ���� ������ ������ ��������. ���� ĸ���� ������ �ذ�.
/// S9*�ٴڿ� ��°��� �ִϸ��̼� �������� ����.
/// </�ذ��>
///  >>>>>>>>�̵� ���� �ڵ��� ���� ������ �ʿ��� ����. �Ǵ� �׿��� �ذ� ����� ����ؾ� �� ���� ����,
/// 
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
    public float slipRate;
    #endregion
    #region private ������Ʈ
    Rigidbody2D rig;
    Animator animator;
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
    #region ����� ���� ����
    int moveState = 0;
    enum CharState
	{
        None = -1,
        Normal,
        Cling,
        WallJumpR,
        WallJumpL,

	}

    #endregion
    #region raycast ��
    RaycastHit2D rayHitR;
    RaycastHit2D rayHitL;
    float rayLen = 1.0f;
    int ignoreLayer = 7;
    #endregion
	void Start()
    {
        ignoreLayer = -1 & ~(7 << (ignoreLayer-2));
        defaultSpeed = speed;
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("LookDir", true);
    }

    void Update()
    {
        if(moveState == (int)CharState.WallJumpR)
		{
            Debug.Log("���������� ����");
            rig.velocity = new Vector2(defaultSpeed, rig.velocity.y);
		} //������
        else if( moveState == (int)CharState.WallJumpL)
		{
            Debug.Log("����");
            rig.velocity = new Vector2(-defaultSpeed, rig.velocity.y);
		} //����
		else if (moveState == (int)CharState.Cling)
		{
            
            rig.gravityScale = 0;
            rig.AddForce(Vector2.down * Time.deltaTime * slipRate);
            rayHitR = Physics2D.Raycast(transform.position, Vector2.right, rayLen, ignoreLayer);
            Debug.DrawRay(transform.position, Vector2.right, Color.red);
            Debug.Log(rayHitR.transform);
            rayHitL = Physics2D.Raycast(transform.position, Vector2.left, rayLen, ignoreLayer);
            Debug.DrawRay(transform.position, Vector2.left, Color.blue);
            Debug.Log(rayHitL.transform);
			WallJump();
		} // ������ ���� �ڵ��
		else
		{
            animator.SetBool("isGrounded", isGrounded);
            animator.SetBool("isJump", false);
            animator.SetFloat("X", rig.velocity.x);
            animator.SetFloat("Y", rig.velocity.y);
            float hor = Input.GetAxis("Horizontal");
            if (hor > 0)
            {
                animator.SetBool("LookDir", false);
                animator.SetBool("isIdle", false);
                animator.SetBool("isRun", true);
            }

            if (hor < 0)
            {
                animator.SetBool("LookDir", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isRun", true);
            }

            if (Mathf.Approximately(rig.velocity.x, 0))
            {
                animator.SetBool("isIdle", true);
                animator.SetBool("isRun", false);
            }

            v = new Vector2(hor * defaultSpeed, rig.velocity.y);
            rig.velocity = v;
            if (Input.GetKeyDown(KeyCode.S))
            {
                rig.velocity += Vector2.down * 0.001f;
            }

            DetectDash();
            Jump();
        } // �Ϲ� ���� �ڵ��
        
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
            rig.AddForce(Vector2.left * dashPower * declinedDashSpeed, ForceMode2D.Impulse);
            

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
            rig.AddForce(Vector2.right * dashPower * declinedDashSpeed, ForceMode2D.Impulse);
            
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

    void WallJump()
	{
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
		{
            rig.gravityScale = 1;
            if (rayHitR.transform != null) //�����ʿ� ��ü
            {
                moveState = (int)CharState.WallJumpL;
            }
            else if(rayHitL.transform != null) //���ʿ� ��ü
            {
                moveState = (int)CharState.WallJumpR;
            }
            if(rayHitL.transform != null || rayHitR.transform != null)
			{
                rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
            
            
        }
	}
    void Jump()
    {
        if (!sPressed)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(rig.velocity.y);
                if (!isJump && isGrounded && rig.velocity.y <= 0)
                {
                    rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    Debug.Log("����");
                    isJump = true;
                    animator.SetBool("isJump", true);
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
        if(col.gameObject.layer == 8 && (Physics2D.Raycast(transform.position, Vector2.left, rayLen, ignoreLayer) || Physics2D.Raycast(transform.position, Vector2.right, rayLen, ignoreLayer)))
		{
            Debug.Log("����");
            moveState = (int)CharState.Cling;
            rig.velocity = Vector2.zero;
            isJump = false;
		}
        else if ((col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Fallable")) && rig.velocity.y <= 0)
        {
            Debug.Log("����");
            isJump = false;
            moveState = (int)CharState.Normal;
            rig.gravityScale = 1;
        }
    }
	private void OnCollisionStay2D(Collision2D collision)
	{
        if (sPressed && spacePressed )
        {
            StartCoroutine(CollisionOn());
        }
    }
}
