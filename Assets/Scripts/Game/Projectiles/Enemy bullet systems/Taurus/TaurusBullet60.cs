using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet60 : ScriptableEnemyBullet<TaurusBulletSystem61, Laser>
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 1f;

        yield return WaitForSeconds(1.5f);

        float z = 90f * Mathf.Sign(transform.position.x);
        Vector3 pos = transform.position;

        var bullet = SpawnBullet(0, z, pos, false);
        bullet.transform.parent = transform;
        bullet.Fire();
    }
}