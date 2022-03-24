using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
///if (dashTime <= 0)
///{
///    defaultSpeed = speed;
///    Debug.Log("��ó�. ��ýð� : " + (Time.time - Debugnum1));

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
/// ��ý� ����Ʈ���ǵ带 ��ý��ǵ�� ���ϰ�, v
/// ��ýð��� ���ٰ�v
/// 0���� �۾�����v
/// �ӵ��� ���󺹱� v
/// ��û��°� �Ǹ�
/// ���Ÿ���� ����ƮŸ������ ����
/// 
/// 
/// 
/// ############ ���� �ʿ� ##############
/// �� ����� ������
/// aa + d(aa ��� ���, firstkey ����) + "gap �̻��� �ð�" <- ���⼭ �÷��̾��� ��￣ d�� �������� ����.
/// + dd <- �÷��̾�� dd ��ø� ����Ϸ� ��. �׷��� �ý��ۻ� �Ʊ� ���� firstkey ������ ��.(false)
/// <- 2��° d���� �ٽ� firstkey�� ����
/// => "gap �̻��� �ð�" ���� �ƹ��͵� ����/d �������� �̵��� ����ϰ������ d(firstkey ����)d(firstkey Ű��)d(���)�� ������ ��.
/// ����ݱ� �� ��ҷ� �����?
/// -> +��̿��,  +������� ����
/// ��ø� ���߷����� �����ϱ�? (��ÿ� ���� �ֱ�)
/// -> +���ǰ�, -���۰�
/// ��� ĵ��Ű �ֱ�?
/// -> +����, -Ű���� ����
/// ��� ��Ŀ���� ����(��÷� ������ �ӵ��� �ݴ���⿡�� Ȱ�밡��)����?
/// -> +������ ����, -������
/// 
/// </summary>
public class PlayerController : MonoBehaviour
{
	#region public ����
	public float DashGap;
    public float speed;
    public float dashSpeed;
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
    bool isDash = false;
    float dashTime = 1f;
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