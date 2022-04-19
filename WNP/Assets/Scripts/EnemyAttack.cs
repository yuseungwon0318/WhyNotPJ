using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enemy가 때리면 플레이어가 사라지는 코드 작성 예정
/// </summary>
public class EnemyAttack : MonoBehaviour
{
    GameObject enemyAttackRangeLeft;
    GameObject enemyAttackRangeRight;
    public GameObject enemyAttackRangeLeftPut;
    public GameObject enemyAttackRangeRightPut;

    void Start()
    {
        enemyAttackRangeLeft = Instantiate(enemyAttackRangeLeftPut);
        enemyAttackRangeRight = Instantiate(enemyAttackRangeRightPut);
    }

    void Update()
    {
        enemyAttackRangeLeft.transform.position = transform.position;
        enemyAttackRangeRight.transform.position = transform.position;
    }
}
