using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiShooter01 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 4;
    const int BulletCount = 10;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 0.5f;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = PlayerPosition.GetRotationDifference(transform.position);
                float t = ii * BulletSpacing;
                Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(t);

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount / 2));

                SpawnProjectile(1, z, pos).Fire();
            }
        }
    }
}