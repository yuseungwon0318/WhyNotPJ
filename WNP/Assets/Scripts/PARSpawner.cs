using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PARSpawner : MonoBehaviour
{
    public GameObject playerAttackRange;
    Transform transform;

    void Start()
    {
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(playerAttackRange, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }
}
