using System.Collections;
using UnityEngine;

public class LeoBullet07 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float r = Random.Range(3f, 5f);
        MoveSpeed = r;
        StartCoroutine(this.LerpDirection(Vector3.down.RotateVectorBy(Random.Range(-30f, 30f)), r * 0.1f));
        yield return this.LerpSpeed(r, 2f, 2f);
    }
}