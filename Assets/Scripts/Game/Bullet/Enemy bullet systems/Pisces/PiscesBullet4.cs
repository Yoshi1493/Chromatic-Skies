using System.Collections;
using UnityEngine;

public class PiscesBullet4 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(6f, 3f, 2f));
        yield return this.RotateBy(-33f, 3f);
    }
}