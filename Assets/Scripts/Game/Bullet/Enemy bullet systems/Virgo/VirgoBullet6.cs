using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirgoBullet6 : EnemyBullet
{
    protected override float MaxLifetime => 1.8f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}