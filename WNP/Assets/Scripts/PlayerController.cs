using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 점프 낙하 구현 완료. 
/// 지금껏 사용하던 collision2d는 충돌자체를 정보로 가지기에
/// 충돌이 다시 일어나면 충돌정보가 바뀜.
/// 그래서 collider2d로 받아와 처리.
/// 
/// 
/// 
/// 
/// #########문제발생##############
/// 조작을 하지 않는 상태에서 S + 스페이스를 누를시
/// 내려감이 먹히지 않음
/// 문제의 원인 : 위에 적힌대로 collision2d가 충돌을 정보로 가져서
/// 아무 조작도 안하면 갱신이 안되니 태그비교에서 막힘. (지금 아래 코드 218줄에서 주석처리한부분)
/// 해결하려면 태그비교를 없애고 레이어비교로 전환해야하는데, 지금 추락플랫폼에 사용중인 플랫폼이펙터2D는 레이어를 안먹음
/// 전용 특수레이어로 충돌은 비교해야하는데, 이걸 처음해보는거라 난관에 부딪힘.
/// 도움!
/// </summary>
public class PlayerController : MonoBehaviour
{
	#region public 변수
    public float JumpGap = 0.5f;
	public float DashGap;
    public float speed;
    public float dashPower;
    [Tooltip("대시 지속시간을 나타냄")]
    public float defaultTime;
    public float jumpPower;
    #endregion
    #region private 컴포넌트
    Rigidbody2D rig;
    CapsuleCollider2D myCol;
    Collider2D firstFloor;
    PlatformEffector2D myPlat;
	#endregion
	#region 대시관련 변수들
    float jumpTimer = 0f;
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

	#endregion
	void Start()
    {
        defaultSpeed = speed;
        myCol = GetComponent<CapsuleCollider2D>();
        rig = GetComponent<Rigidbody2D>();
        myPlat = GetComponent<PlatformEffector2D>();
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
		if (Input.GetKey(KeyCode.Space))
		{
            spacePressed = true;
		}
		else
		{
            spacePressed = false;
		}
        DetectDash();
        Jump();
    }

    void FixedUpdate()
    {
        Dash();
        Debug.Log(rig.velocity.y);
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
        if (Input.GetKey(KeyCode.W) || spacePressed)
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

    IEnumerator CollisionOn(Collider2D collider)
	{
		///////무시
        yield return new WaitForSeconds(1f);
        ///////복원

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
        if (sPressed && spacePressed /*&& collision.gameObject.CompareTag("Fallable")*/)
        {
            firstFloor = collision.collider;
            
            StartCoroutine(CollisionOn(firstFloor));
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