using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBullet5 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;

        yield return WaitUntil(() => FindObjectsOfType<AriesBullet5>().Length == 48);
        yield return WaitForSeconds(0.5f);

        StartCoroutine(this.RotateAround(playerShip, 10f, 60f));
    }
}