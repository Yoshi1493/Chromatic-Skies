using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class PiscesShooter05 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 2;
    const int BulletCount = 16;
    const float BulletSpawnRadius = 0.8f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < RepeatCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(RandomAngleDeg);
                float z = PlayerPosition.GetRotationDifference(pos);

                SpawnProjectile(5, z, pos).Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);
        }
    }
}