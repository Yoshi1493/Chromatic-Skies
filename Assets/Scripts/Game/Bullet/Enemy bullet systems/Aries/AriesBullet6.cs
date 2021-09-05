using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBullet6 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return WaitUntil(() => FindObjectsOfType<AriesBullet6>().Length == 16);

        StartCoroutine(this.RotateAround(playerShip, 5f, 60f));
    }
}