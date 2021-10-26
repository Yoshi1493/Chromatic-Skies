using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBullet2 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;

        yield return WaitForSeconds(1f);

        moveDirection = -transform.right;

        yield return this.LerpSpeed(0f, 3f, 1f);
    }
}