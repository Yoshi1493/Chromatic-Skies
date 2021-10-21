using System.Collections;
using UnityEngine;

public class AriesBullet4 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return StartCoroutine(this.LerpSpeed(3f, 0f, 1));

        this.LookAt(playerShip);
        StartCoroutine(this.LerpSpeed(0f, 5f, 0f, 0.5f));
    }
}