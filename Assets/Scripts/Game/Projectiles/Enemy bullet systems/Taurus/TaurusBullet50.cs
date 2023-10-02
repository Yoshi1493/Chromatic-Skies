using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet50 : ScriptableEnemyBullet<TaurusBulletSystem5, EnemyBullet>
{
    const int BulletCount = 5;
    const float ShootingCooldown = 0.1f;

    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.RotateBy(180f, ShootingCooldown * BulletCount, false));

        for (int i = 0; i < BulletCount; i++)
        {
            float z = transform.eulerAngles.z;
            Vector3 pos = transform.position;

            SpawnBullet(1, z, pos, false).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}