using System.Collections;
using UnityEngine;

public class PiscesBullet4 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return StartCoroutine(this.LerpSpeed(4f, 0f, 1f));

        StartCoroutine(this.LerpSpeed(0f, 4f, 0f, 1f));
        this.LookAt(playerShip);
    }
}