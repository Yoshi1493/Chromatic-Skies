using System.Collections;
using UnityEngine;

public class AriesBullet03 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        Vector3 d = moveDirection;
        yield return this.LerpSpeed(3f, 0f, 1f);

        this.LookAt(playerShip);
        yield return this.LerpSpeed(2.5f, 0f, 1f);

        moveDirection = d;
        yield return this.LerpSpeed(2f, 3f, 1f);
    }
}