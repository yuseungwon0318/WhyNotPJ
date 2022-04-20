using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectRange : MonoBehaviour
{
    public bool detected;

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            detected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detected = false;
    }
}
