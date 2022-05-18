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
/// 현재 수정해야하는 상황 :
/// 
/// 
/// 
/// 
/// <해결됨> 
/// P1*붙잡기벽이 위/아래에서도 닿을 수 있을때, 위아래에서 닿은 경우 붙잡기가 활성화.
/// 
/// P2*잡기 불가 벽이 있을 경우, 거기에 비비면 공중에 뜸.
/// 
/// P3*계단 픽셀이 조금 맞지 않음. 자를때의 문제일 수도 있다.
/// 
/// P4*플랫폼과 붙잡기벽에 동시에 닿은 상태에서, 벽점프가 활성화 즉시 취소됨.
/// 그러므로 가속도가 중간에 멈춰서 어색한 운동을 보임.
/// 플랫폼도 바닥으로 치는 데다가, 플랫폼과 닿으면 벽점프가 일반상태로 전환됨.
/// 
/// P5*계단 오브젝트와 닿아있다가 평지로 넘어가면 조금 하늘로 뜸. (안고칠거면 상관없음.)
/// 관성의 법칙 때문에 그런건데, 이동 방식을 전면 수정해야할 필요가 보임.
/// 현재 이동은 힘을 가하여 속도를 임의로 변화시키면서 하고 있는데,
/// 그렇기에 위 현상을 막기 위해 리지드바디의 축을 잠그면 이동도 묶임.
/// 
/// P6*플랫폼이펙터 사용중 아래에서 점프했을 때, 플랫폼에 스치면
/// 아주 잠시동안 isJump가 비활성화됨.
/// 지금은 점프조건을 위로 향하는 힘이 없을 때로 정해놓아서 문제없지만,
/// 이후에 애매한 상황이 나올 수도 있음.
/// 
/// P7* 벽에 비비기는 안되는데, 벽에 비비고 바닥에 내려가면 점프상태가 해제되지 않음.
/// 계단에 올라서 조금 공중에 뜨면 다시 점프가 가능해짐. ( 점프 상태가 해제됨 )
/// 
/// P8*계단 등 빗길에서 점프가 안먹힘.
/// Y축 속도가 있어서 그런것일 수도 있다.
/// 
/// P9*계단에서 점프후 애니메이션이 낙하중으로 남아있음.
/// 세로방향으로 속도가 더해지는것 때문에 그런듯.
/// 
/// P10*애니메이터.
/// 간략화하긴 했는데 점프가 어색하다. 스페이스바를 누르면 체크를 진행 & 바닥에 닿으면 탈출
/// 다 괜찮은데 정점에 도달했을때 2번그림이 나와야함.
/// 정점 체크가 어렵다.
/// 
/// P11*벽점프 후 걸림.
/// 레이캐스트 위치가 중심으로 설정되어있어서 그렇다.
/// 발위치로 해주면 납득가는 판정이 날듯.
/// 
/// P12*
/// 점프중 대시 모션이 안뜸. 애니메이터 문제인데, 조건이 두 가지 모두 충족되어서
/// 둘중 하나만 나오는 상황임.  해결하려면 우선순위를 부여하거나 추가적인 조건을 넣어야 하는데,
/// 전자는 되는지 모르겠고 후자는 생각할 시간이 필요함.
/// 
/// P13*점프중 일반바닥에 닿으면 걸릴 수 있음.
/// 
/// P14*플랫폼을 통과할 때 착지판정이 뜸.
/// 
/// P15*벽에 지면과 평행한 대시로 붙으면 어색한 상황이 연출됨.
/// 점프시 수직으로 날아오르고 대시가 가능함.
/// movestate가 0임.
/// 
/// P16*대시에 추가적인 조건을 넣어야 할 듯?
/// 스태미나 등.
/// 대시를 연속해서 하면 속도가 너무 빠름.
/// 대시키를 연속해서 누르면 잔상이 계속 활성화됨.
/// 
/// P17*애니메이션 오류 = fall 상태로 고정됨.
/// 아주 잠깐 공중에 떴다가 떨어지면서 fall이 됐는데, 다른곳으로 갈 수 없어서 그런듯.
/// 
/// P18*계단에 대해서 대시를 사용할 때, 조건에 따라 아래 3가지 상황중 1개가 발생.
/// 가만히 있다가 대시 + 대시로 계단을 벗어나지 못함 -> 애니메이션 오류/슈퍼점프 (조작여부에 따라 갈림.)
/// 이동하다가 대시 -> 슈퍼점프
/// 대시중 방향전환 -> 슈퍼점프
/// 
/// P19*
/// 간헐적으로 플레이어와 붙잡기벽간의 충돌이 실패함.
/// 플레이어가 벽에 비비는 행동을 막기 위해 추가한 충돌만 검출이 일어나고 안쪽에 있는 플레이어와 충돌은 발생하지 않음.
/// 벽에 비비는 행동을 막기 위한 다른 방법을 찾거나, 안쪽 플레이어와의 충돌을 일으킬 방법을 찾아야 함.
/// 
/// P20*붙잡기벽에 대시로 접근한 뒤 빠르게 점프 커맨드를 입력하면 벽점프가 일부 먹힘.
/// 벽점프중 X속도가 간헐적으로 0으로 변화 + 동시에 Y속도가 크게 감소.
/// 상태에는 문제가 없음.
/// 벽점프 속도에도 문제는 없음.
/// 아마도 다른 곳에서 속도가 변하는 듯?
/// 
/// 
/// S1*레이캐스트를 사용해 물체가 있는지에 대해 정보를 판단해서 상태를 결정.
/// S2*추가 collider를 붙여서 해결. raycast를 사용하기에 별 문제 없었음.
/// S3*자를 때 검은 부분이 조금 같이 잘림. 크기를 2픽셀정도 줄여서 해결.
/// S4*위 방향으로 힘이 가해지면 착지하지 못하게 함.
/// S5*수정하려고 노력중 -> 해결됨. (S*18)
/// S6*타협함. --> 해결됨. (S*14)
/// S7*다른 문제 해결중 해결됨.
/// S8*플레이어 발이 작은 원형이기에 땅에 닿은지 판정이 실패했음. 발을 캡슐로 넓혀서 해결.
/// S9*바닥에 닿는것을 애니메이션 조건으로 삼음.
/// S10*점프모션을 타협함.
/// S11*바닥에 닿는 판정 메커니즘을 변경. 전보다 조금 빠르면서, 넓은 판정을 사용할 수 있어서 해결.
/// S12*애니메이터에 has exit time 의 문제였음
/// S13*미끄러운 충돌을 조금 더 넓힘.
/// S14*Y축 방향으로 힘이 가해지면 착지판정을 안나오게 함. 추가로 P6도 해결.
/// S15*대시 끝나면 상태를 일반으로 고쳐줘서 생긴 문제. 대시 종료시 상태판정을 한번 돌려서 해결.
/// S16*더함.
/// S17*계단을 올라가는 상태는 항상 바닥에 닿아 있을 것이므로 계단상태에서 바닥에 닿았는지 체크를 항상 TRUE로 함.
/// S18*대시 종료시 계단에 닿아있다면 Y방향 속도를 초기화함.
/// S19*모든 일반 벽의 마찰을 0으로 함.
/// S20*벽에 붙거나 점프를 실행하면 즉시 대시가 취소되게 함.
/// </해결됨>
/// 
/// </summary>
public class PlayerController : MonoBehaviour
{
	#region public 변수
    public static PlayerController Instance;
    public UnityEvent<float> OnDash;

    public Transform Feet;
	public float DashGap;
    public int DashFull; //최대 대시 충전량
    public float DashCooldown;
    public float speed;
    public float dashPower;
    [Tooltip("대시 지속시간을 나타냄")]
    public float defaultTime;
    public float jumpPower;
    public float slipRate;
    

    public Rigidbody2D  rig;
    #endregion
    #region private 컴포넌트
    Animator animator;
    #endregion
    #region 이동&대시관련 변수들
    public int DashCount; //현재 대시 갯수 (UI에서 쓰기 위해 public임. 값 수정은 영향 없음.
    Vector2 v;
    float hor;
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
    bool resetDash = false;
    int dCountChangedAmount = 0;
	#endregion
	#region 점프/낙하관련 변수들
	bool isJump = false;
    bool sPressed = false;
    bool spacePressed = false;
    public bool isGrounded = false;
    public bool isFall = false;
    public bool fallchanged = false;
    #endregion
    #region 운동상태 관련 변수
    CharState moveState = 0;
    enum CharState
	{
        None = -1,
        Normal,
        Cling,
        WallJumpR, //우상향 점프
        WallJumpL, //좌상향 점프
        StairWalk,
    }
    #endregion
    #region raycast 등
    RaycastHit2D rayHitR;
    RaycastHit2D rayHitL;
    float rayLen = 1f;
    int ignoreLayer = 7; // 착지를 비롯한 레이캐스트 감지 용도. 플레이어만 무시함.
    int useLayerCling = 8; // 벽붙잡기 용도. 오로지 Stickable만 감지함.
	#endregion

	#region 유니티기본
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
	#region 조작관련

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
    }//각종 초기화

    void Look(Vector2 dir)
	{
        Vector2 rotation = new Vector2(dir.y, Mathf.Max( 0,dir.x)) * 180;
        transform.eulerAngles = rotation;
	} //방향을 지정. vector2.right, vector2.left를 사용. 각각 180, 0도. (Y)

	#region 이동관련 함수

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
        } // 벽잡은 상태 코드들
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
                }//충돌 활성화용 코드. 더 나은 방안이 필요.
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
            if (rayHitR.transform != null) //오른쪽에 물체
            {
                
                moveState = CharState.WallJumpL;
            }
            else if(rayHitL.transform != null) //왼쪽에 물체
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
        yield return null; // 1프레임 대기
        fallchanged = false;
    } //낙하
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
