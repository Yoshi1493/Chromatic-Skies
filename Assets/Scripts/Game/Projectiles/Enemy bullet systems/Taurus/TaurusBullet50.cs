using System.Collections;
using UnityEngine;

public class TaurusBullet50 : EnemyBullet
{
    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        yield break;
    }
}