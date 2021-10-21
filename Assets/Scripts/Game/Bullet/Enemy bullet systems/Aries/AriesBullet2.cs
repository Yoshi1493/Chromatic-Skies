using System.Collections;
using UnityEngine;

public class AriesBullet2 : EnemyBullet
{
    protected override float MaxLifetime => 3f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}