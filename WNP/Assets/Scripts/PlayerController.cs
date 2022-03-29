using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 대시 메커니즘 변경 사항 : 
/// 대시중 속도를 더하도록 함.
/// 대시중 가속도를 1.1로 나눠서 제동을 검.
/// firstkeypressed를 방향별로 만듬.
/// 대시 방향을 나눔.
/// firstkeypressedA, D 각각 초기화 부분을 따로 만듬
/// 
/// 1. 이동중 방향 전환후 대시
/// 2. 이동중 대시
/// 3. 정지상태에서 대시
/// 4. 대시 종료후 대시
/// 5. 대시중 대시
/// ####모두 의도한 대로 작동함을 확인함.####
/// 
/// 점프/낙하 변경사항 : (임시)
/// 레이어를 만들고 레이어간 충돌을 무효화시키도록 함.
/// -> 다시 충돌을 판정시키지는 못함
/// ####이후 개선 필요####
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
    #endregion
    #region private 컴포넌트
    Rigidbody2D rig;
    CapsuleCollider2D myCol;
	#endregion
	#region 대시관련 변수들
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
        if (Input.GetKeyDown(KeyCode.Space) && sPressed)
        {
            Physics2D.IgnoreLayerCollision(7,6);
        }
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
        if (Input.GetKeyUp(KeyCode.A) && !firstKeyPressedA)
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
        if (Input.GetKeyUp(KeyCode.D) && !firstKeyPressedD)
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
}