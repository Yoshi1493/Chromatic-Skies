using System.Collections;
using UnityEngine;

public class LibraBullet62 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        Vector3 d = moveDirection;

        yield return this.LerpSpeed(5f, 0f, 1f);

        this.LookAt(playerShip);
        yield return this.LerpSpeed(2.5f, 0f, 1f);

        this.LookAt(playerShip);
        yield return this.LerpSpeed(2.5f, 0f, 1f);

        moveDirection = d;
        MoveSpeed = 2f;
    }
}