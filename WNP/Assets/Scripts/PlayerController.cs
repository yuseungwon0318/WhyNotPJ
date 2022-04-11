using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
/// <summary>
/// coroutine에 대한 설명.
/// 아래 IEnumerator라고 적혀있는 함수가 코루틴이다.
/// 코루틴은 일반 함수와는 다르게 리턴값을 여러번 줄 수 있는데,
/// 이것도 그냥 리턴값이 아니고 특정 기능을 하도록 리턴시킬수 있다.(yield return)
/// 주로 사용되는건 시간(초)재기나, 입력이 들어올때 까지 기다리기 등이다.
/// 코루틴을 사용하는 이유는 간편해서도 있지만 동시처리가 가능해서가 가장 크다.
/// 
/// collidermask에 대한 설명,
/// 컴퓨터 내장 계산기에서 프로그래머모드 있는데
/// 그거열어보면 이해가 편함.
/// 
/// 예를들어 레이어 0~10이 있다고 쳐보겠다.
/// 그렇다면 컴퓨터는 이를 2진수로 저장하기 때문에 00 0000 0000으로 저장함.
/// 이때, 충돌판정을 무시하겠다는 신호는 0, 충돌을 하겠다는 신호는 1이다.
/// 그렇기때문에 비트시프트로 무시할 만큼 밀어서 반대로 해주면 작동하게 된다.
/// 2진수와 음수개념이 머릿속에 완벽히 박히면 저기에 비트시프트 말고 숫자만 넣을수도 있겠지만
/// 그러면 이해가 힘들어지니 비트시프트를 이해하도록 하자.
/// 무시의 반대는 재활성화
/// 재활성화도 마찬가지로 활성화 시킬만큼 밀어서
/// or로 켜주면 된다.
/// 1을 밀면 뒷부분도 전부 1로 나오는 것을 이용한 것으로 보임.
/// ignoreFloor에 관련 코드가 있고, 호출부는 playercontroller의 collisionOn이다.
/// </summary>
public class PlayerController : MonoBehaviour
{
	#region public 변수
	public float DashGap;
    public float speed;
    public float dashPower;
    [Tooltip("대시 지속시간을 나타냄")]
    public float defaultTime;
    public float jumpPower;
    public float slipRate;
    #endregion
    #region private 컴포넌트
    Rigidbody2D rig;
    Animator animator;
    #endregion
    #region 대시관련 변수들
    Vector2 v = new Vector2(1,0);
	float keyTime;
    bool ADash = false;
    bool DDash = false;
    float dashTime;
    float declinedDashSpeed = 1f; //대시 가속도 감소량 (백분율)
    float defaultSpeed;
	bool firstKeyPressedA = false;
    bool firstKeyPressedD = false;
    bool resetA = false;
    bool resetD = false;
	#endregion
	#region 점프/낙하관련 변수들
	bool isJump = false;
    bool sPressed = false;
    bool spacePressed = false;
    public static bool isGrounded = false;
    public bool isFall = false;
    public bool fallchanged = false;
    #endregion
    #region 운동상태 관련 변수
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
    #region raycast 등
    RaycastHit2D rayHitR;
    RaycastHit2D rayHitL;
    float rayLen = 1.0f;
    int ignoreLayer = 7;
    #endregion
	void Start()
    {
        ignoreLayer = -1 & ~(1 << ignoreLayer);
        defaultSpeed = speed;
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("LookDir", true);
    }

    void Update()
    {
        if(moveState == (int)CharState.WallJumpR)
		{
            rig.velocity = new Vector2(defaultSpeed, rig.velocity.y);
		} //오른점
        else if( moveState == (int)CharState.WallJumpL)
		{
            rig.velocity = new Vector2(-defaultSpeed, rig.velocity.y);
		} //왼점
		else if (moveState == (int)CharState.Cling)
		{
            
            rig.gravityScale = 0;
            rig.AddForce(Vector2.down * Time.deltaTime * slipRate);
            rayHitR = Physics2D.Raycast(transform.position, Vector2.right, rayLen, ignoreLayer);
            rayHitL = Physics2D.Raycast(transform.position, Vector2.left, rayLen, ignoreLayer);
			WallJump();
		} // 벽잡은 상태 코드들
		else
		{
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
        } // 일반 상태 코드들
        
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
            if (rayHitR.transform != null) //오른쪽에 물체
            {
                moveState = (int)CharState.WallJumpL;
            }
            else if(rayHitL.transform != null) //왼쪽에 물체
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
                if (!isJump && isGrounded && rig.velocity.y <= 0)
                {
                    rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
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
        yield return null; // 1프레임 대기
        fallchanged = false;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8)
		{
            moveState = (int)CharState.Cling;
            rig.velocity = Vector2.zero;
            isJump = false;
		}
        else if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Fallable"))
        {
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
	private void OnCollisionExit2D(Collision2D collision)
	{
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Fallable"))
        {
            isJump = true;
        }
    }
}
