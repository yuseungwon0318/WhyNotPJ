using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enemy�� Enemy�� ���� ������ ���� detected�� false�� ��
/// </summary>
public class EnemyDetectRange : MonoBehaviour
{
    public GameObject enemy;
    public bool detected;

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detected = true;
        }
        else
        {
            detected = false;
        }
    }
}
