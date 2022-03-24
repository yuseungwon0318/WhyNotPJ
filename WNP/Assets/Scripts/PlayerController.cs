using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
///if (dashTime <= 0)
///{
///    defaultSpeed = speed;
///    Debug.Log("대시끝. 대시시간 : " + (Time.time - Debugnum1));

///    if (isDash)
///    {
///        dashTime = defaultTime;
///    }
///}
///        else
///{
///    dashTime -= Time.deltaTime;
///    defaultSpeed = dashSpeed;
///}
///isDash = false;
///
/// 대시시 디폴트스피드를 대시스피드로 정하고, v
/// 대시시간을 빼다가v
/// 0보다 작아지면v
/// 속도를 원상복구 v
/// 대시상태가 되면
/// 대시타임을 디폴트타임으로 설정
/// 
/// 
/// 
/// ############ 논의 필요 ##############
/// 현 대시의 문제점
/// aa + d(aa 대시 취소, firstkey 켜짐) + "gap 이상의 시간" <- 여기서 플레이어의 기억엔 d가 남아있지 않음.
/// + dd <- 플레이어는 dd 대시를 사용하려 함. 그러나 시스템상 아까 켜진 firstkey 판정이 들어감.(false)
/// <- 2번째 d에서 다시 firstkey가 켜짐
/// => "gap 이상의 시간" 동안 아무것도 안함/d 방향으로 이동후 대시하고싶으면 d(firstkey 끄기)d(firstkey 키기)d(대시)를 눌려야 함.
/// 도움닫기 등 요소로 만들기?
/// -> +재미요소,  +설명관련 문제
/// 대시를 폭발력으로 변경하기? (대시에 방향 넣기)
/// -> +현실감, -조작감
/// 대시 캔슬키 넣기?
/// -> +편리함, -키세팅 문제
/// 대시 메커니즘 원상(대시로 증가한 속도를 반대방향에도 활용가능)복구?
/// -> +개발의 편함, -개연성
/// 
/// </summary>
public class PlayerController : MonoBehaviour
{
	#region public 변수
	public float DashGap;
    public float speed;
    public float dashSpeed;
    [Tooltip("대시 지속시간을 나타냄")]
    public float defaultTime;
    public float jumpPower;
    #endregion
    #region private 컴포넌트
    Rigidbody2D rig;
    CapsuleCollider2D myCol;
	#endregion
	#region 대시관련 변수들
	float keyTime;
    bool isDash = false;
    float dashTime = 1f;
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
	#endregion
	void Start()
    {
        defaultSpeed = speed;

        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float hor = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(hor * defaultSpeed, rig.velocity.y);

        if (Input.GetKey(KeyCode.S))
        {
            sPressed = true;
        }
        else
        {
            sPressed = false;
        }
        DetectDash();
        Jump();
    }

    void FixedUpdate()
    {
        Dash();
    }

    void Dash()
	{
        if (dashTime <= 0)
        {
            isDash = false;
        }
        if (isDash)
		{
            defaultSpeed = dashSpeed;
            dashTime -= Time.deltaTime;
		}
		else
		{
            defaultSpeed = speed;
		}
        
	}

    void DetectDash()
    {
        if (Input.GetKeyDown(KeyCode.A) && firstKeyPressedA)
        {
			
            if (Time.time < keyTime + DashGap)
            {
                isDash = true;
                dashTime = defaultTime;
            }
            resetA =true;
        }
        if (Input.GetKeyDown(KeyCode.A) && !firstKeyPressedA)
        {
            if (isDash)
                isDash = false;
            firstKeyPressedA = true;
            keyTime = Time.time;
            resetD = true;
            
        }
        

        if (Input.GetKeyDown(KeyCode.D) && firstKeyPressedD)
        {
            
            if (Time.time < keyTime + DashGap)
			{
                isDash = true;
                dashTime = defaultTime;
            }

            resetD = true;
        }
        if (Input.GetKeyDown(KeyCode.D) && !firstKeyPressedD)
        {
            if (isDash)
                isDash = false;
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

    void Jump()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            if (sPressed == false)
            {
                if (isJump == false)
                {
                    rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

                    isJump = true;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Ground" && rig.velocity == Vector2.zero)
        {
            isJump = false;
        }
    }

    void OnCollisionStay2D(Collision2D colS)
    {
        if ((sPressed == true) && Input.GetKey(KeyCode.Space))
        {
            GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D triE)
    {
        GetComponent<CapsuleCollider2D>().isTrigger = false;
    }
}