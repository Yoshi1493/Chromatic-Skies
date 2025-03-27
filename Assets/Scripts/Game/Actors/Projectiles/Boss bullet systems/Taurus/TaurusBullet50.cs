using System.Collections;
using UnityEngine;

public class TaurusBullet50 : BossBullet
{
    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}