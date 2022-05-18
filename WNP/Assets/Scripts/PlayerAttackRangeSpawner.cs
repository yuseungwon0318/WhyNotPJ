using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRangeSpawner : MonoBehaviour
{
    public GameObject playerAttackRange;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(playerAttackRange, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }
}
