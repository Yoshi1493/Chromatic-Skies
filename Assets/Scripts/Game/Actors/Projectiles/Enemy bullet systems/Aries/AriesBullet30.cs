using System.Collections;
using UnityEngine;

public class AriesBullet30 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        Vector3 d = moveDirection;
        yield return this.LerpSpeed(3f, 0f, 1f);

        this.LookAt(playerShip);
        yield return this.LerpSpeed(3f, 0f, 1f);

        moveDirection = d;
        MoveSpeed = 2.5f;
    }
}