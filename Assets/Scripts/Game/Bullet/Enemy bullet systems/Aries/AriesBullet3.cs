using System.Collections;
using UnityEngine;

public class AriesBullet3 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return StartCoroutine(this.LerpSpeed(3f, 0f, 1));

        StartCoroutine(this.LookAt(playerShip, 0.5f));
        StartCoroutine(this.LerpSpeed(0f, 5f, 0f, 0.5f));
    }
}