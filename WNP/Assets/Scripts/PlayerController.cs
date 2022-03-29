using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��� ��Ŀ���� ���� ���� : 
/// ����� �ӵ��� ���ϵ��� ��.
/// ����� ���ӵ��� 1.1�� ������ ������ ��.
/// firstkeypressed�� ���⺰�� ����.
/// ��� ������ ����.
/// firstkeypressedA, D ���� �ʱ�ȭ �κ��� ���� ����
/// 
/// 1. �̵��� ���� ��ȯ�� ���
/// 2. �̵��� ���
/// 3. �������¿��� ���
/// 4. ��� ������ ���
/// 5. ����� ���
/// ####��� �ǵ��� ��� �۵����� Ȯ����.####
/// 
/// ����/���� ������� : (�ӽ�)
/// ���̾ ����� ���̾ �浹�� ��ȿȭ��Ű���� ��.
/// -> �ٽ� �浹�� ������Ű���� ����
/// ####���� ���� �ʿ�####
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
    #endregion
    #region private ������Ʈ
    Rigidbody2D rig;
    CapsuleCollider2D myCol;
	#endregion
	#region ��ð��� ������
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