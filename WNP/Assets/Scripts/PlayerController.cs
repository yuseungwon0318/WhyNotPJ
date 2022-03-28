using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// {
/// 점프시 현재 속도를 판정해서 점프여부를 판단중.
/// 이동중 점프 + 천장에 부딪힘 = 점프 불가
/// --> 천장에 닿은 뒤 내려오면서 좌우방향 속도가 증가/감소하기에 점프 판정이 끝나지 않음.
/// }
/// 해결됨. 플레이어의 발에 추가로 스크립트를 붙여 땅에 떨어졌는지를 판단케함.
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
    Collision2D firstFloor;
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
        Jump();
    }

    void FixedUpdate()
    {
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

    IEnumerator CollisionOn(Collision2D collision)
	{
        
        yield return new WaitForSeconds(1f);
        Debug.Log("원상복구");
        Physics2D.IgnoreCollision(myCol,collision.collider,false); //문제 발생. 충돌 판정이 리셋이 안되고 닿아있는 것과 판정됨.
        
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
            Debug.Log("추락");
            Physics2D.IgnoreCollision(myCol, collision.collider);
            firstFloor = collision;
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