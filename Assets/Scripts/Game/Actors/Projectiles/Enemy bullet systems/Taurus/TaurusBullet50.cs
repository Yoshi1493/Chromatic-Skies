using System.Collections;
using UnityEngine;

public class TaurusBullet50 : ScriptableEnemyBullet<TaurusBulletSystem52, Laser>
{
    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}