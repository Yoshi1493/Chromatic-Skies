using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoShooter00 : CommonEnemyShooter<EnemyBullet>
{
    const int BulletCount = 2;

    protected override float ShootingCooldown => 3f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = PlayerPosition.GetRotationDifference(transform.position);
            Vector3 pos = Vector3.zero;

            SpawnProjectile(0, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}