using System.Collections;
using UnityEngine;

public class PiscesBullet60 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(0f, endSpeed, 1f);
    }
}