using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enemy�� ������ �÷��̾ ������� �ڵ� �ۼ� ����
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
