using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enemy가 Enemy의 감지 범위에 들어가면 detected가 false가 됨
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
