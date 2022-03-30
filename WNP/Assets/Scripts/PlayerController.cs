using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 점프 낙하 구현 완료. 
/// 지금껏 사용하던 collision2d는 충돌자체를 정보로 가지기에
/// 충돌이 다시 일어나면 충돌정보가 바뀜.
/// 그래서 collider2d로 받아와 처리.
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
    }

    void FixedUpdate()
    {
        Dash();
        if (Input.GetKey(KeyCode.Space) && isGrounded && jumpTimer <= 0f)
        {
            Jump();
            jumpTimer = JumpGap;
        }
        jumpTimer -= Time.fixedDeltaTime;
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
                if (!isJump && isGrounded)
                {
                    rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    isJump = true;
                }
            }
        }
    }

    IEnumerator CollisionOn(Collider2D col)
	{
        Physics2D.IgnoreCollision(myCol, col);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(myCol, col, false); //문제 발생. 충돌 판정이 리셋이 안되고 닿아있는 것과 판정됨.

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
        if (sPressed && spacePressed && collision.gameObject.CompareTag("Fallable"))
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