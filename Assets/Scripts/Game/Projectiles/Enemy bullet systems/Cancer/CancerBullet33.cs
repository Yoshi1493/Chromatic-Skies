using System.Collections;
using UnityEngine;

public class CancerBullet33 : EnemyBullet
{
    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        transform.parent = ownerShip.GetComponentInChildren<CancerBulletSystem31>().transform;
        yield return this.LerpSpeed(4f, 0f, 1f);
    }
}