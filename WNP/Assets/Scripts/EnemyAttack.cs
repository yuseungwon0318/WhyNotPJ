using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    GameObject enemyAttackRange;
    GameObject target;
    public GameObject enemyAttackRangePut;
    public GameObject enemy;

    void Start()
    {
        target = GameObject.Find("Player");
        enemyAttackRange = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if (enemyAttackRange.GetComponent<EnemyAttackRange>().attacked == true)
        {
            Debug.Log("Enemy Attack");
        }
    }
}
