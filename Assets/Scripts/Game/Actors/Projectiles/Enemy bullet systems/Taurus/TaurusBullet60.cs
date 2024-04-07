using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet60 : ScriptableEnemyBullet<TaurusBulletSystem61, Laser>
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return EndOfFrame;

        float z = 90f * Mathf.Sign(transform.position.x);
        Vector3 pos = transform.position;

        var bullet = SpawnBullet(0, z, pos, false);
        bullet.transform.parent = transform;
        bullet.Fire();

        yield return this.LerpSpeed(5f, 1f, 2f);
    }
}