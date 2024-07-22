using System.Collections;
using UnityEngine;

public class AriesBullet31 : EnemyBullet
{
    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}