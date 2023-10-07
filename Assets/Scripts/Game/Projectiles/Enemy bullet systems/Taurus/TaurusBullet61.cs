using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet61 : ScriptableEnemyBullet<TaurusBulletSystem62, Laser>
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 2f;

        yield return WaitForSeconds(1f);

        float z = transform.eulerAngles.z + 90f;
        Vector3 pos = transform.position;

        var bullet = SpawnBullet(0, z, pos, false);
        bullet.transform.parent = transform;
        bullet.Fire();

        yield return null;
    }
}