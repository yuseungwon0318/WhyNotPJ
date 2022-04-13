using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Vector3 dir;
    GameObject enemyDetectRange;
    GameObject target;
    public GameObject enemyDetectRangePut;
    public float speed = 2;

    void Start()
    {
        target = GameObject.Find("Player");
        enemyDetectRange = Instantiate(enemyDetectRangePut);
    }

    void Update()
    {
        dir = target.transform.position - transform.position;
        enemyDetectRange.transform.position = transform.position;

        dir.Normalize();

        if(enemyDetectRange.GetComponent<EnemyDetectRange>().detected == true)
        {
            transform.position += dir * speed * Time.deltaTime;
        }
    }
}
