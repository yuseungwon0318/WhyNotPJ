using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;
using UnityEngine.UIElements;
/// <summary>
/// ���� �����ؾ��ϴ� ��Ȳ :
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
/// P10*�ִϸ�����.
/// ����ȭ�ϱ� �ߴµ� ������ ����ϴ�. �����̽��ٸ� ������ üũ�� ���� & �ٴڿ� ������ Ż��
/// �� �������� ������ ���������� 2���׸��� ���;���.
/// ���� üũ�� ��ƴ�.
/// 
/// P11*������ �� �ɸ�.
/// ����ĳ��Ʈ ��ġ�� �߽����� �����Ǿ��־ �׷���.
/// ����ġ�� ���ָ� ���氡�� ������ ����.
/// 
/// P12*
/// ������ ��� ����� �ȶ�. �ִϸ����� �����ε�, ������ �� ���� ��� �����Ǿ
/// ���� �ϳ��� ������ ��Ȳ��.  �ذ��Ϸ��� �켱������ �ο��ϰų� �߰����� ������ �־�� �ϴµ�,
/// ���ڴ� �Ǵ��� �𸣰ڰ� ���ڴ� ������ �ð��� �ʿ���.
/// 
/// P13*������ �Ϲݹٴڿ� ������ �ɸ� �� ����.
/// 
/// P14*�÷����� ����� �� ���������� ��.
/// 
/// P15*���� ����� ������ ��÷� ������ ����� ��Ȳ�� �����.
/// ������ �������� ���ƿ����� ��ð� ������.
/// movestate�� 0��.
/// 
/// P16*��ÿ� �߰����� ������ �־�� �� ��?
/// ���¹̳� ��.
/// ��ø� �����ؼ� �ϸ� �ӵ��� �ʹ� ����.
/// ���Ű�� �����ؼ� ������ �ܻ��� ��� Ȱ��ȭ��.
/// 
/// P17*�ִϸ��̼� ���� = fall ���·� ������.
/// ���� ��� ���߿� ���ٰ� �������鼭 fall�� �ƴµ�, �ٸ������� �� �� ��� �׷���.
/// 
/// P18*��ܿ� ���ؼ� ��ø� ����� ��, ���ǿ� ���� �Ʒ� 3���� ��Ȳ�� 1���� �߻�.
/// ������ �ִٰ� ��� + ��÷� ����� ����� ���� -> �ִϸ��̼� ����/�������� (���ۿ��ο� ���� ����.)
/// �̵��ϴٰ� ��� -> ��������
/// ����� ������ȯ -> ��������
/// 
/// P19*
/// ���������� �÷��̾�� ����⺮���� �浹�� ������.
/// �÷��̾ ���� ���� �ൿ�� ���� ���� �߰��� �浹�� ������ �Ͼ�� ���ʿ� �ִ� �÷��̾�� �浹�� �߻����� ����.
/// ���� ���� �ൿ�� ���� ���� �ٸ� ����� ã�ų�, ���� �÷��̾���� �浹�� ����ų ����� ã�ƾ� ��.
/// 
/// P20*����⺮�� ��÷� ������ �� ������ ���� Ŀ�ǵ带 �Է��ϸ� �������� �Ϻ� ����.
/// �������� X�ӵ��� ���������� 0���� ��ȭ + ���ÿ� Y�ӵ��� ũ�� ����.
/// ���¿��� ������ ����.
/// ������ �ӵ����� ������ ����.
/// �Ƹ��� �ٸ� ������ �ӵ��� ���ϴ� ��?
/// 
/// 
/// S1*����ĳ��Ʈ�� ����� ��ü�� �ִ����� ���� ������ �Ǵ��ؼ� ���¸� ����.
/// S2*�߰� collider�� �ٿ��� �ذ�. raycast�� ����ϱ⿡ �� ���� ������.
/// S3*�ڸ� �� ���� �κ��� ���� ���� �߸�. ũ�⸦ 2�ȼ����� �ٿ��� �ذ�.
/// S4*�� �������� ���� �������� �������� ���ϰ� ��.
/// S5*�����Ϸ��� ����� -> �ذ��. (S*18)
/// S6*Ÿ����. --> �ذ��. (S*14)
/// S7*�ٸ� ���� �ذ��� �ذ��.
/// S8*�÷��̾� ���� ���� �����̱⿡ ���� ������ ������ ��������. ���� ĸ���� ������ �ذ�.
/// S9*�ٴڿ� ��°��� �ִϸ��̼� �������� ����.
/// S10*��������� Ÿ����.
/// S11*�ٴڿ� ��� ���� ��Ŀ������ ����. ������ ���� �����鼭, ���� ������ ����� �� �־ �ذ�.
/// S12*�ִϸ����Ϳ� has exit time �� ��������
/// S13*�̲����� �浹�� ���� �� ����.
/// S14*Y�� �������� ���� �������� ���������� �ȳ����� ��. �߰��� P6�� �ذ�.
/// S15*��� ������ ���¸� �Ϲ����� �����༭ ���� ����. ��� ����� ���������� �ѹ� ������ �ذ�.
/// S16*����.
/// S17*����� �ö󰡴� ���´� �׻� �ٴڿ� ��� ���� ���̹Ƿ� ��ܻ��¿��� �ٴڿ� ��Ҵ��� üũ�� �׻� TRUE�� ��.
/// S18*��� ����� ��ܿ� ����ִٸ� Y���� �ӵ��� �ʱ�ȭ��.
/// S19*��� �Ϲ� ���� ������ 0���� ��.
/// S20*���� �ٰų� ������ �����ϸ� ��� ��ð� ��ҵǰ� ��.
/// </�ذ��>
/// 
/// </summary>
public class PlayerController : MonoBehaviour
{
	#region public ����
    public static PlayerController Instance;
    public UnityEvent<float> OnDash;

    public Transform Feet;
	public float DashGap;
    public int DashFull; //�ִ� ��� ������
    public float DashCooldown;
    public float speed;
    public float dashPower;
    [Tooltip("��� ���ӽð��� ��Ÿ��")]
    public float defaultTime;
    public float jumpPower;
    public float slipRate;
    

    public Rigidbody2D  rig;
    #endregion
    #region private ������Ʈ
    Animator animator;
    #endregion
    #region �̵�&��ð��� ������
    public int DashCount; //���� ��� ���� (UI���� ���� ���� public��. �� ������ ���� ����.
    Vector2 v;
    float hor;
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
    bool resetDash = false;
    int dCountChangedAmount = 0;
	#endregion
	#region ����/���ϰ��� ������
	bool isJump = false;
    bool sPressed = false;
    bool spacePressed = false;
    public bool isGrounded = false;
    public bool isFall = false;
    public bool fallchanged = false;
    #endregion
    #region ����� ���� ����
    CharState moveState = 0;
    enum CharState
	{
        None = -1,
        Normal,
        Cling,
        WallJumpR, //����� ����
        WallJumpL, //�»��� ����
        StairWalk,
    }
    #endregion
    #region raycast ��
    RaycastHit2D rayHitR;
    RaycastHit2D rayHitL;
    float rayLen = 1f;
    int ignoreLayer = 7; // ������ ����� ����ĳ��Ʈ ���� �뵵. �÷��̾ ������.
    int useLayerCling = 8; // ������� �뵵. ������ Stickable�� ������.
	#endregion

	#region ����Ƽ�⺻
	void Start()
    {
        InitAll();
        StartCoroutine(DashCharge());
    } 

    void Update()
    {
        animator.SetInteger("moveState", (int)moveState);
        WallCling();
        StairWalk();
		Move();
    }
    void FixedUpdate()
    {
        sPressed =  DetectS();
        spacePressed =  DetectSpace();
        Dash();
    }
	#endregion
	#region ���۰���

	bool DetectS()
	{
        if (Input.GetKey(KeyCode.S))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool DetectSpace()
	{
        if (Input.GetKey(KeyCode.Space))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

	#endregion

	void InitAll()
	{
        Instance = this;
        ignoreLayer = ~(1 << ignoreLayer);
        useLayerCling = 1 << useLayerCling;
        defaultSpeed = speed;
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        DashCount = DashFull;
    }//���� �ʱ�ȭ

    void Look(Vector2 dir)
	{
        Vector2 rotation = new Vector2(dir.y, Mathf.Max( 0,dir.x)) * 180;
        transform.eulerAngles = rotation;
	} //������ ����. vector2.right, vector2.left�� ���. ���� 180, 0��. (Y)

	#region �̵����� �Լ�

    void StairWalk()
	{
        if(moveState == CharState.StairWalk)
		{
            if (Physics2D.OverlapCircle(Feet.position, 0.5f, ignoreLayer))
			{
                animator.SetBool("isGrounded", true);
                animator.SetFloat("Y", rig.velocity.y);
                    hor = Input.GetAxis("Horizontal");
                    if (hor > 0)
                    {
                        Look(Vector2.right);
                        animator.SetBool("isIdle", false);
                        animator.SetBool("isRun", true);
                    }

                    if (hor < 0)
                    {
                        Look(Vector2.left);
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
                DetectDash();
		    }
            else
            {
                moveState = CharState.Normal;
                rig.velocity = Vector2.zero;
            }
        }
		
	}

	void WallCling()
	{
        if (moveState == CharState.Cling)
        {
            resetDash = false;
            animator.SetBool("isRun", false);
            animator.SetBool("isIdle", false);
            rig.gravityScale = 0;
            rig.AddForce(Vector2.down * Time.deltaTime * slipRate);
            rayHitR = Physics2D.Raycast(Feet.position, Vector2.right, rayLen, useLayerCling);
            rayHitL = Physics2D.Raycast(Feet.position, Vector2.left, rayLen, useLayerCling);
            if (!rayHitL && !rayHitR)
            {
                rig.gravityScale = 1;
                moveState = (int)CharState.Normal;
            }
			if (rayHitL)
			{
                Look(Vector2.left);
			}
            else if (rayHitR)
			{
                Look(Vector2.right);
			}
            WallJump();
            WallJumpCancel();
        } // ������ ���� �ڵ��
    }

    void WallJumpCtrl()
	{
        if (moveState == CharState.WallJumpR)
        {
            ResetDash();
            Look(Vector2.right);
            rig.velocity = new Vector2(defaultSpeed + hor, rig.velocity.y);
        }
        else if (moveState == CharState.WallJumpL)
		{
            ResetDash();
			Look(Vector2.left);
            rig.velocity = new Vector2(-defaultSpeed + hor, rig.velocity.y);
        }
    }

    void ResetDash()
	{
        resetA = true;
        resetD = true;
        resetDash = true;
        dashTime = -1;
    }

    void MoveAndAnim()
	{
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("Y", rig.velocity.y);
        if (hor > 0)
        {
            Look(Vector2.right);
            animator.SetBool("isIdle", false);
            animator.SetBool("isRun", true);
        }

        if (hor < 0)
        {
            Look(Vector2.left);
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
        
    }

    void Move()
	{
        if (moveState != CharState.Cling)
        {
            hor = Input.GetAxis("Horizontal");
            rig.gravityScale = 1;
            WallJumpCtrl();
            if (moveState == CharState.Normal)
            {
                
                MoveAndAnim();
                DetectDash();
                Jump();
                if (Input.GetKeyDown(KeyCode.S))
                {
                    rig.velocity += Vector2.left * 0.001f;
                }//�浹 Ȱ��ȭ�� �ڵ�. �� ���� ����� �ʿ�.
            } 
        }
    }

    void Dash()
	{
        if(moveState != CharState.WallJumpL && moveState != CharState.WallJumpR)
		{
            if (ADash)
            {
                if (dashTime <= 0 || resetDash)
                {
                    
                    ADash = false;
                    resetD = true;
                    declinedDashSpeed = 1;
                }
				if (ADash)
				{
                    Look(Vector2.left);
                    OnDash.Invoke(transform.eulerAngles.y);
                    DDash = false;
                    rig.AddForce(Vector2.left * dashPower * declinedDashSpeed, ForceMode2D.Impulse);
                    dashTime -= Time.deltaTime;
                    declinedDashSpeed /= 1.1f;
                }
            }
            else if (DDash)
            {
                if (dashTime <= 0 || resetDash)
                {
                    
                    DDash = false;
                    resetA = true;
                    declinedDashSpeed = 1;
                }
				if (DDash)
				{
                    Look(Vector2.right);
                    OnDash.Invoke(transform.eulerAngles.y);
                    ADash = false;
                    rig.AddForce(Vector2.right * dashPower * declinedDashSpeed, ForceMode2D.Impulse);
                    dashTime -= Time.deltaTime;
                    declinedDashSpeed /= 1.1f;
                }
                
                

            }
        }
        
        
	}

    void DetectDash()
    {
        if(DashCount > 0 && !ADash && !DDash)
		{
            resetDash =false;
            if (Input.GetKeyDown(KeyCode.A) && firstKeyPressedA)
            {
                
                if (Time.time < keyTime + DashGap)
                {
                    DashCount -= 1;
                    dCountChangedAmount += 1;
                    StartCoroutine(DoubleKey());
                    ADash = true;
                    dashTime = defaultTime;
                }
                resetA = true;
            }
            if (Input.GetKey(KeyCode.A) && !firstKeyPressedA)
            {
                firstKeyPressedA = true;
                keyTime = Time.time;
                resetD = true;
            }


            if (Input.GetKeyDown(KeyCode.D) && firstKeyPressedD)
            {

                if (Time.time < keyTime + DashGap)
                {
                    DashCount -= 1;
                    dCountChangedAmount += 1;
                    keyTime = 0;
                    StartCoroutine(DoubleKey());
                    DDash = true;
                    dashTime = defaultTime;
                }

                resetD = true;
            }
            if (Input.GetKey(KeyCode.D) && !firstKeyPressedD)
            {
                firstKeyPressedD = true;
                keyTime = Time.time;
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
        
    }

    void WallJump()
	{
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
		{
            rig.gravityScale = 1;
            if (rayHitR.transform != null) //�����ʿ� ��ü
            {
                
                moveState = CharState.WallJumpL;
            }
            else if(rayHitL.transform != null) //���ʿ� ��ü
            {
                moveState = CharState.WallJumpR;
            }
            if(rayHitL.transform != null || rayHitR.transform != null)
			{
                rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                resetA = true;
                resetD = true;
            }
            
            
        }
	}

    void WallJumpCancel()
	{
        if(moveState == CharState.Cling)
		{
            if (rayHitL && Input.GetKeyDown(KeyCode.D))
            {
                moveState = CharState.Normal;
                transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
            }
            else if (rayHitR && Input.GetKeyDown(KeyCode.A))
            {
                moveState = CharState.Normal;
                transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
            }
        }
        
    }

    void Jump()
    {
        if (!sPressed)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {

                if (!isJump && isGrounded && rig.velocity.y <= 0)
                {
                    rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    isJump = true;
                }
            }
        }
    }

    void DetectCling()
	{
        if(Physics2D.Raycast(Feet.position, Vector2.left, rayLen, ignoreLayer) 
            || Physics2D.Raycast(Feet.position, Vector2.right, rayLen, ignoreLayer))
        {
            moveState = CharState.Cling;
            rig.velocity = Vector2.zero;
            isJump = false;
        }

	}
	#endregion

	#region IEnumerator
	IEnumerator DashCharge()
	{
		while (true)
		{
            yield return null;
            if (DashCount < DashFull && dCountChangedAmount > 0)
            {
                yield return new WaitForSeconds(DashCount);
                DashCount += 1;
                --dCountChangedAmount;
            }
        }
        
	}

    

    IEnumerator DoubleKey()
	{
        animator.SetBool("doubleKeyPress", true);
        animator.SetBool("isDash", true);
        yield return null;
        animator.SetBool("doubleKeyPress", false);
        yield return new WaitForSeconds(defaultTime);
        animator.SetBool("isDash", false);
    }

    IEnumerator CollisionOn()
	{
        fallchanged = true;
		isFall =true;
        yield return new WaitForSeconds(0.7f);
        isFall = false;
        yield return null; // 1������ ���
        fallchanged = false;
    } //����
    #endregion

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 9 && moveState == CharState.Normal)
		{
			moveState = CharState.StairWalk;

        }
        if(col.gameObject.layer == 8)
		{
            DetectCling();
            
        }
        else if ((col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Fallable")) && rig.velocity.y <= 0)
        {
            isJump = false;
            moveState = CharState.Normal;
            rig.gravityScale = 1;
        }
    }

	private void OnCollisionStay2D(Collision2D collision)
	{
        if (sPressed && spacePressed && moveState == CharState.Normal)
        {
            StartCoroutine(CollisionOn());
        }
		if (collision.gameObject.layer == 9 && moveState == CharState.Normal)
		{
			moveState = CharState.StairWalk;

		}
	}
}
