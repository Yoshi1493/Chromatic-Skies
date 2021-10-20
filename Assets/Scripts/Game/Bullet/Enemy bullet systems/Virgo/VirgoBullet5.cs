using System.Collections;
using UnityEngine;

public class VirgoBullet5 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return null;

        StartCoroutine(this.LerpSpeed(1f, 5f, 1f));
        yield return this.RotateAround(ownerShip, 2f, 180f, delay: 0.5f);
        yield return this.LerpSpeed(MoveSpeed, 5f, 1f);
    }
}