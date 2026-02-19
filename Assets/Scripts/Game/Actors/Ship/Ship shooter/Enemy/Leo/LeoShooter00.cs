using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoShooter00 : CommonEnemyShooter<EnemyBullet>
{
    const int BulletCount = 3;

    protected override float ShootingCooldown => 2.2f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(0.8f);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = PlayerPosition.GetRotationDifference(transform.position);
            Vector3 pos = Vector3.zero;

            SpawnProjectile(0, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}