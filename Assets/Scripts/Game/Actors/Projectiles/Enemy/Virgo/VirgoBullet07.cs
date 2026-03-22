using System.Collections;
using UnityEngine;

public class VirgoBullet07 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpDirection(Vector3.down.RotateVectorBy(Random.Range(-15f, 15f)), 2f));
        yield return this.LerpSpeed(6f, 2f, 0.5f);
    }
}