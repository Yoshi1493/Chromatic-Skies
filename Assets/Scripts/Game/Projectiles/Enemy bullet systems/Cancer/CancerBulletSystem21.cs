using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CancerBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 24;
    const int BulletCount = 4;
    const float BulletSpawnRadius = 0.5f;

    protected override float ShootingCooldown => 0.04f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = RandomAngleDeg;
                Vector3 pos = BulletSpawnRadius * Random.insideUnitCircle;
                SpawnProjectile(4, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}