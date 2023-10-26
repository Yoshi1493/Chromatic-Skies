using System.Collections;
using UnityEngine;

public class AquariusBullet61 : EnemyBullet
{
    protected override float MaxLifetime => 7f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}