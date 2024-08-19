using System.Collections;
using UnityEngine;
using static MathHelper;

public class ScorpioBullet60 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        Vector3 v = transform.position - (3f * transform.up.RotateVectorBy(PositiveOrNegativeOne * Random.Range(60f, 80f)));
        yield return this.MoveTo(v, 1f);
    }
}