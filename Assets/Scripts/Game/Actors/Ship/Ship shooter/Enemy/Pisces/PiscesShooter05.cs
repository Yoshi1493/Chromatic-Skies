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
        for (int i = 0; i < RepeatCount; i++)
        {
            yield return WaitForSeconds(0.6f);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(RandomAngleDeg);
                float z = PlayerPosition.GetRotationDifference(transform.position + pos);

                SpawnProjectile(0, z, pos).Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}