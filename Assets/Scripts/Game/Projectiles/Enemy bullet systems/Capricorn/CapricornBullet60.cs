using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBullet60 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(endSpeed, 0f, 0.5f);
        yield return this.LerpSpeed(0f, endSpeed, 1f);
    }
}